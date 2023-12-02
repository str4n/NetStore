using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _catalogsDbContext.Products.ToListAsync();

    public async Task AddAsync(Product product)
        => await _catalogsDbContext.Products.AddAsync(product);

    public Task UpdateAsync(Product product)
    {
        _catalogsDbContext.Products.Update(product);

        return Task.CompletedTask;
    }
}