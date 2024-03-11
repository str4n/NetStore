using MassTransit;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Commands;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Orders.Application.Messaging;

internal sealed class CreateCheckoutCartConsumer : IConsumer<CreateCheckoutCart>
{
    private readonly ICheckoutRepository _repository;
    private readonly IMessageBroker _messageBroker;

    public CreateCheckoutCartConsumer(ICheckoutRepository repository, IMessageBroker messageBroker)
    {
        _repository = repository;
        _messageBroker = messageBroker;
    }
    
    public async Task Consume(ConsumeContext<CreateCheckoutCart> context)
    {
        var userId = context.Message.UserId;
        
        var checkoutCart = new CheckoutCart(userId);

        await _repository.AddAsync(checkoutCart);
        await _messageBroker.PublishAsync(new CheckoutCartCreated(userId));
    }
}