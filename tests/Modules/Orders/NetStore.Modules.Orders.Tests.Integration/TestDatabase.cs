using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Infrastructure.EF;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Infrastructure.EF;
using NetStore.Modules.Payments.Core.EF;
using NetStore.Tests.Shared.Integration;

namespace NetStore.Modules.Orders.Tests.Integration;

internal sealed class TestDatabase : IDisposable
{
    public OrdersDbContext OrdersDbContext { get; }
    public PaymentsDbContext PaymentsDbContext { get; }
    public CatalogsDbContext CatalogsDbContext { get; }

    public TestDatabase()
    {
        OrdersDbContext = new OrdersDbContext(DatabaseHelper.GetOptions<OrdersDbContext>());
        PaymentsDbContext = new PaymentsDbContext(DatabaseHelper.GetOptions<PaymentsDbContext>());
        CatalogsDbContext = new CatalogsDbContext(DatabaseHelper.GetOptions<CatalogsDbContext>());
        
        SeedDatabase();
    }

    public void Dispose()
    {
        OrdersDbContext?.Database.EnsureDeleted();
        OrdersDbContext?.Dispose();

        PaymentsDbContext?.Database.EnsureDeleted();
        PaymentsDbContext?.Dispose();

        CatalogsDbContext?.Database.EnsureDeleted();
        CatalogsDbContext?.Dispose();
    }

    private void SeedDatabase()
    {
        var id = Guid.Parse("00000000-0000-0000-0000-000000000001");
        
        CatalogsDbContext.Products.Add(Product.Create(id, "mock", "mock", 1, 1, "mock", "mock", Gender.Male, AgeCategory.Adult, Size.M, Color.Black, "mock"));
        CatalogsDbContext.SaveChanges();

        OrdersDbContext.Carts.Add(new Cart(id));
        OrdersDbContext.Products.Add(new Domain.Product.Product(id, "mock", "mock", "M", "Black", 999));
        OrdersDbContext.SaveChangesAsync();
    }
}