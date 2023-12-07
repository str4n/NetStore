using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class CartRepository : ICartRepository
{
    private readonly OrdersDbContext _dbContext;

    public CartRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Cart cart)
    {
        await _dbContext.Carts.AddAsync(cart);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Cart> GetAsync(Guid customerId)
        => _dbContext.Carts.SingleOrDefaultAsync(x => x.CustomerId == customerId);
}