namespace NetStore.Modules.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order.Order order);
}