using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Catalogs.Domain.Brand;
using NetStore.Modules.Catalogs.Domain.Category;
using NetStore.Modules.Catalogs.Domain.Product;

namespace NetStore.Modules.Catalogs.Infrastructure.EF;

internal sealed class CatalogsDbContext : DbContext
{
    public DbSet<Product> ProductPatterns { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }

    public CatalogsDbContext(DbContextOptions<CatalogsDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalogs");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}