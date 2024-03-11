using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Types.Aggregate;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly CatalogsDbContext _dbContext;

    public ProductRepository(CatalogsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Product> GetAsync(Guid id)
        => _dbContext.Products.SingleOrDefaultAsync(x => x.Id == (AggregateId)id);

    public async Task<IEnumerable<Product>> GetAllAsync(bool tracking = true)
        => tracking
            ? await _dbContext.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .ToListAsync()
            
            : await _dbContext.Products
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .ToListAsync();

    public async Task<IEnumerable<Product>> GetAllByCategoryAsync(string category, bool tracking = true)
        => tracking
            ? await _dbContext.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Category.Name == category)
                .ToListAsync()
            
            : await _dbContext.Products
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Category.Name == category)
                .ToListAsync();

    public async Task<IEnumerable<Product>> GetAllByBrandAsync(string brand, bool tracking = true)
        => tracking
            ? await _dbContext.Products
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Brand.Name == brand)
                .ToListAsync()
            
            : await _dbContext.Products
                .AsNoTracking()
                .Include(x => x.Brand)
                .Include(x => x.Category)
                .Where(x => x.Brand.Name == brand)
                .ToListAsync();

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DecreaseStockAsync(Guid productId, int amount)
    {
        var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == (AggregateId)productId);
        
        product.DecreaseStock(amount);

        await _dbContext.SaveChangesAsync();
    }
}