using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using NetStore.Modules.Catalogs.Domain.Brand;
using NetStore.Modules.Catalogs.Domain.Category;
using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Services;
using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Tests.Shared.Integration;
using NetStore.Tests.Shared.Integration.Endpoints;
using Xunit;

namespace NetStore.Modules.Orders.Tests.Integration.Endpoints;

[Collection("integration")]
public class CheckoutCartEndpointsTests : EndpointsTests, IDisposable
{
    [Fact]
    public async Task Get_Checkout_Cart_Without_Being_Authorized_Should_Return_Unauthorized()
    {
        var response = await Client.GetAsync(Route);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_Checkout_Cart_Should_Return_Proper_One()
    {
        Authorize(Guid.Parse(Id));
        
        var response = await Client.GetAsync(Route);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var checkoutCart = await response.Content.ReadFromJsonAsync<CheckoutCartDto>();
        
        checkoutCart.Products.Should().ContainSingle();
    }
    
    
    private const string Route = "orders-module/checkout";
    private const string Id = "00000000-0000-0000-0000-000000000001";
    private readonly TestDatabase _testDatabase;

    public CheckoutCartEndpointsTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
        SeedDatabase();
    }

    public void Dispose()
    {
        _testDatabase?.Dispose();
    }
    
    private void SeedDatabase()
    {
        try
        {
            var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var product = Product.Create(id, "mock", "mock description", 1, 1, "mock", "mock", Gender.Male,
                AgeCategory.Adult, Size.M, Color.Black, "mock");

            var category = Category.Create("Mock", "Mock description");
            var brand = Brand.Create("Mock");

            new ProductDomainService().SetProductPrice(product, 999);
            
            product.IncreaseStock(10);

            _testDatabase.CatalogsDbContext.Categories.Add(category);
            _testDatabase.CatalogsDbContext.Brands.Add(brand);
            _testDatabase.CatalogsDbContext.Products.Add(product);
            _testDatabase.CatalogsDbContext.SaveChanges();

            var oProduct = new Domain.Product.Product(id, "mock", "mock", "M", "Black",
                product.GrossPrice);

            oProduct.Stock = 10;
            
            _testDatabase.OrdersDbContext.Products.Add(oProduct);

            var cart = new Cart(id);
            
            cart.AddProduct(oProduct);
            
            _testDatabase.OrdersDbContext.Carts.Add(cart);

            _testDatabase.OrdersDbContext.CheckoutCarts.Add(cart.Checkout());
            
            _testDatabase.OrdersDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            Dispose();
        }
    }
}