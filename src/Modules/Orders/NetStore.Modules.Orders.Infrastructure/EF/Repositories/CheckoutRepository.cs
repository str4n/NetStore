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
        => _dbContext.CheckoutCarts
            .Include(x => x.Products)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.Id == id);

    public Task<CheckoutCart> GetByCustomerId(Guid id)
        => _dbContext.CheckoutCarts
            .Include(x => x.Products)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.CustomerId == id);

    public async Task UpdateAsync(CheckoutCart checkoutCart)
    {
        _dbContext.CheckoutCarts.Update(checkoutCart);
        await _dbContext.SaveChangesAsync();
    }


    public async Task AddAsync(CheckoutCart checkoutCart)
    {
        await _dbContext.CheckoutCarts.AddAsync(checkoutCart);
        await _dbContext.SaveChangesAsync();
    }
}