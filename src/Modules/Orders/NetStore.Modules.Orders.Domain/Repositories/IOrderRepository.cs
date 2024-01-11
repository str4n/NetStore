namespace NetStore.Modules.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order.Order order);
    Task<Order.Order> GetAsync(Guid id);
    Task UpdateAsync(Order.Order order);
}