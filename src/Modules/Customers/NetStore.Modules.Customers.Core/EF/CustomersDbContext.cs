using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Customers.Core.Domain.Entities;
using NetStore.Modules.Customers.Core.Domain.ValueObjects;

namespace NetStore.Modules.Customers.Core.EF;

internal sealed class CustomersDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public CustomersDbContext(DbContextOptions<CustomersDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.HasDefaultSchema("customers");
    }
}