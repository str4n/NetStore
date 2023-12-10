using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class CheckoutRepository : ICheckoutRepository
{
    private readonly OrdersDbContext _dbContext;

    public CheckoutRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<CheckoutCart> GetAsync(Guid id)
        => _dbContext.CheckoutCarts.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(CheckoutCart checkoutCart)
    {
        await _dbContext.CheckoutCarts.AddAsync(checkoutCart);
        await _dbContext.SaveChangesAsync();
    }
}