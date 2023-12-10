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

    public async Task<IEnumerable<Product>> GetAvailableAsync(string codeName, int quantity)
    {
        var products = _dbContext.Products.Where(x => x.State != ProductState.Ordered && x.CodeName == codeName);
        
        var result = await products.Take(quantity).ToListAsync();

        return result;
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }
}