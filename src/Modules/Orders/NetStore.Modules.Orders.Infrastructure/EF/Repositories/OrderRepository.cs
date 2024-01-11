using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Orders.Domain.Order;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Types.Aggregate;

namespace NetStore.Modules.Orders.Infrastructure.EF.Repositories;

internal sealed class OrderRepository : IOrderRepository
{
    private readonly OrdersDbContext _dbContext;

    public OrderRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Order> GetAsync(Guid id)
        => _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == (AggregateId)id);

    public async Task UpdateAsync(Order order)
    {
        _dbContext.Update(order);
        await _dbContext.SaveChangesAsync();
    }
}