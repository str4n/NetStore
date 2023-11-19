using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Products.Core.Domain.Entities;

namespace NetStore.Modules.Products.Core.EF;

internal sealed class ProductsDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.HasDefaultSchema("products");
    }
}