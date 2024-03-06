using MassTransit;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Commands;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Orders.Application.Messaging;

internal sealed class CreateCartConsumer : IConsumer<CreateCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly IMessageBroker _messageBroker;

    public CreateCartConsumer(ICartRepository cartRepository, IMessageBroker messageBroker)
    {
        _cartRepository = cartRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task Consume(ConsumeContext<CreateCart> context)
    {
        var userId = context.Message.UserId;
        
        var cart = new Cart(userId);

        await _cartRepository.AddAsync(cart);
        await _messageBroker.PublishAsync(new CartCreated(userId));
    }
}