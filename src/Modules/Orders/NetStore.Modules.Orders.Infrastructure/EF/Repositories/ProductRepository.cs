using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Orders.Domain.Product;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class ProductRepository : IProductRepository
{
    private readonly OrdersDbContext _dbContext;

    public ProductRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Product> GetAsync(Guid id)
        => _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);

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

    public async Task DecreaseStockAsync(Guid productId, int quantity)
    {
        await _dbContext
            .Products
            .Where(x => x.Id == productId)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Stock, p => p.Stock - quantity));

        await _dbContext.SaveChangesAsync();
    }
}