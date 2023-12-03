using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Catalogs.Domain.Product.Mockup;
using NetStore.Modules.Catalogs.Domain.Repositories;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Repositories;

internal sealed class ProductMockupRepository : IProductMockupRepository
{
    private readonly CatalogsDbContext _catalogsDbContext;

    public ProductMockupRepository(CatalogsDbContext catalogsDbContext)
    {
        _catalogsDbContext = catalogsDbContext;
    }

    public Task<ProductMockup> GetAsync(long id)
        => _catalogsDbContext.ProductMockups.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<ProductMockup>> GetAllAsync()
        => await _catalogsDbContext.ProductMockups.ToListAsync();

    public async Task AddAsync(ProductMockup mockup)
        => await _catalogsDbContext.ProductMockups.AddAsync(mockup);

    public Task UpdateAsync(ProductMockup mockup)
    {
        _catalogsDbContext.ProductMockups.Update(mockup);

        return Task.CompletedTask;
    }
}