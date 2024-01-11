using NetStore.Modules.Orders.Domain.Exceptions;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Domain.Services;

internal sealed class OrderDomainService : IOrderDomainService
{
    private readonly IOrderRepository _orderRepository;

    public OrderDomainService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task PayOrder(Guid orderId)
    {
        var order = await _orderRepository.GetAsync(orderId);

        if (order is null)
        {
            throw new OrderNotFoundException(orderId);
        }
        
        order.Pay();

        await _orderRepository.UpdateAsync(order);
    }
}