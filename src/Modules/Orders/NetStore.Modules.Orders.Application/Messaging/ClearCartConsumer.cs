using MassTransit;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Commands;

namespace NetStore.Modules.Orders.Application.Messaging;

internal sealed class ClearCartConsumer : IConsumer<ClearCart>
{
    private readonly ICartRepository _cartRepository;

    public ClearCartConsumer(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    
    public async Task Consume(ConsumeContext<ClearCart> context)
    {
        var cart = await _cartRepository.GetByCustomerIdAsync(context.Message.CustomerId);
        
        cart.Clear();

        await _cartRepository.UpdateAsync(cart);
    }
}