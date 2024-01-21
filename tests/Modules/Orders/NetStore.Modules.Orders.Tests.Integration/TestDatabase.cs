using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Catalogs.Domain.Brand;
using NetStore.Modules.Catalogs.Domain.Category;
using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Services;
using NetStore.Modules.Catalogs.Infrastructure.EF;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Services;
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
    }

    public void Dispose()
    {
        OrdersDbContext.Database.EnsureDeleted();
        OrdersDbContext?.Dispose();

        PaymentsDbContext.Database.EnsureDeleted();
        PaymentsDbContext?.Dispose();

        CatalogsDbContext.Database.EnsureDeleted();
        CatalogsDbContext?.Dispose();
    }
}