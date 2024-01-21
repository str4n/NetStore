using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Catalogs.Domain.Brand;
using NetStore.Modules.Catalogs.Domain.Category;
using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Services;
using NetStore.Modules.Catalogs.Infrastructure.EF;
using NetStore.Modules.Orders.Application.Commands;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Infrastructure.EF;
using NetStore.Modules.Payments.Core.EF;
using NetStore.Tests.Shared.Integration;
using Xunit;

namespace NetStore.Modules.Orders.Tests.Integration.Endpoints;

public class CartEndpointsTests : EndpointsTests, IDisposable
{
    [Fact]
    public async Task Add_Product_To_Cart_Without_Being_Authorized_Should_Return_Unauthorized()
    {
        var productId = Guid.Parse(Id);
        var response = await Client.PostAsJsonAsync(Route, new AddProductToCart(productId, 1));

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Add_Single_Product_To_Cart_Should_Succeed()
    {
        var id = Guid.Parse(Id);
        
        Authorize(id);
        
        var response = await Client.PostAsJsonAsync(Route, new AddProductToCart(id, 1));
        
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var cart = await _testDatabase
            .OrdersDbContext
            .Carts
            .Include(cart => cart.Products)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.CustomerId == id);

        cart.Products.Should().ContainSingle();
        cart.Products
            .SingleOrDefault(x => x.ProductId == id)?
            .Quantity.Should().Be(1);
    }
    
    private void Authorize(Guid userId)
    {
        var jwt = AuthHelper.GenerateJwt(userId, "admin", "mock@gmail.com");
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }
    
    private const string Route = "orders-module/cart";
    private const string Id = "00000000-0000-0000-0000-000000000001";
    private readonly TestDatabase _testDatabase;

    public CartEndpointsTests(OptionsProvider optionsProvider, TestApplicationFactory applicationFactory) : base(optionsProvider, applicationFactory)
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
            
            product.IncreaseStock(1);

            _testDatabase.CatalogsDbContext.Categories.Add(category);
            _testDatabase.CatalogsDbContext.Brands.Add(brand);
            _testDatabase.CatalogsDbContext.Products.Add(product);
            _testDatabase.CatalogsDbContext.SaveChanges();

            _testDatabase.OrdersDbContext.Carts.Add(new Cart(id));

            var oProduct = new Domain.Product.Product(id, "mock", "mock", "M", "Black",
                product.GrossPrice);

            oProduct.Stock = 1;
            
            _testDatabase.OrdersDbContext.Products.Add(oProduct);
            
            _testDatabase.OrdersDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            Dispose();
        }
    }
}