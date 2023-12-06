using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Catalogs.Domain.Product.Mockup;
using NetStore.Modules.Catalogs.Domain.Repositories;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Repositories;

internal sealed class ProductMockupRepository : IProductMockupRepository
{
    private readonly CatalogsDbContext _dbContext;

    public ProductMockupRepository(CatalogsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ProductMockup> GetAsync(long id)
        => _dbContext.ProductMockups.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<ProductMockup>> GetAllAsync()
        => await _dbContext.ProductMockups.ToListAsync();

    public async Task AddAsync(ProductMockup mockup)
    {
        await _dbContext.ProductMockups.AddAsync(mockup);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductMockup mockup)
    {
        _dbContext.ProductMockups.Update(mockup);
        await _dbContext.SaveChangesAsync();
    }
}