using MassTransit;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Events;

namespace NetStore.Modules.Orders.Application.Events;

internal sealed class OrderPlacedConsumer : IConsumer<OrderPlaced>
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly ICartRepository _cartRepository;

    public OrderPlacedConsumer(ICheckoutRepository checkoutRepository, ICartRepository cartRepository)
    {
        _checkoutRepository = checkoutRepository;
        _cartRepository = cartRepository;
    }
    
    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        var message = context.Message;

        var cart = await _cartRepository.GetByCustomerIdAsync(message.CustomerId);
        
        cart.Clear();

        await _cartRepository.UpdateAsync(cart);

        await _checkoutRepository.DeleteAsync(message.CustomerId);
    }
}