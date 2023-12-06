using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Order;
using NetStore.Modules.Orders.Domain.Product;

namespace NetStore.Modules.Orders.Infrastructure.EF;

internal sealed class OrdersDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public OrdersDbContext(DbContextOptions<OrdersDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orders");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}