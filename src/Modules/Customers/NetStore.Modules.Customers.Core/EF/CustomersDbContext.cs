using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.Domain.Order;

namespace NetStore.Modules.Customers.Core.EF;

internal sealed class CustomersDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    public CustomersDbContext(DbContextOptions<CustomersDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.HasDefaultSchema("customers");
    }
}