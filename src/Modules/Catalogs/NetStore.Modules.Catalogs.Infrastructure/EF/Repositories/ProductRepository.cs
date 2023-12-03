using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NetStore.Modules.Catalogs.Domain.Brand;
using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Repositories;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly CatalogsDbContext _catalogsDbContext;

    public ProductRepository(CatalogsDbContext catalogsDbContext)
    {
        _catalogsDbContext = catalogsDbContext;
    }

    public Task<Product> GetAsync(Guid id)
        => _catalogsDbContext.Products.SingleOrDefaultAsync(x => x.Id.Value == id);

    public async Task<IEnumerable<Product>> GetAllAsync(bool tracking = true)
        => tracking
            ? await _catalogsDbContext.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .ToListAsync()
            
            : await _catalogsDbContext.Products
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .ToListAsync();

    public async Task<IEnumerable<Product>> GetAllByCategoryAsync(string category, bool tracking = true)
        => tracking
            ? await _catalogsDbContext.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Category.Name == category)
                .ToListAsync()
            
            : await _catalogsDbContext.Products
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Category.Name == category)
                .ToListAsync();

    public async Task<IEnumerable<Product>> GetAllByBrandAsync(string brand, bool tracking = true)
        => tracking
            ? await _catalogsDbContext.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Brand.Name == brand)
                .ToListAsync()
            
            : await _catalogsDbContext.Products
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Brand.Name == brand)
                .ToListAsync();

    public async Task AddAsync(IEnumerable<Product> products)
        => await _catalogsDbContext.Products.AddRangeAsync(products);

    public Task UpdateAsync(Product product)
    {
        _catalogsDbContext.Products.Update(product);

        return Task.CompletedTask;
    }
}