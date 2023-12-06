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
}