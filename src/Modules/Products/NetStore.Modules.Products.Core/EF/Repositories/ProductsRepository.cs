using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Products.Core.Domain.Entities;
using NetStore.Modules.Products.Core.Repositories;

namespace NetStore.Modules.Products.Core.EF.Repositories;

internal sealed class ProductsRepository : IProductsRepository
{
    private readonly ProductsDbContext _dbContext;
    private readonly DbSet<Product> _products;

    public ProductsRepository(ProductsDbContext dbContext)
    {
        _dbContext = dbContext;
        _products = dbContext.Products;
    }
    
    public async Task AddAsync(Product product)
    {
        await _products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Product> GetAsync(Guid id, bool tracking)
        => tracking is false
            ? _products.AsNoTracking().Include(x => x.Categories).SingleOrDefaultAsync(x => x.Id == id)
            : _products.Include(x => x.Categories).SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Product>> GetAllAsync(bool tracking)
        => tracking is false 
            ? await _products.AsNoTracking().ToListAsync() 
            : await _products.ToListAsync();
}