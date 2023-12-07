using MassTransit;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Users.Shared.Events;

namespace NetStore.Modules.Orders.Application.Events;

internal sealed class UserSignedUpConsumer : IConsumer<UserSignedUp>
{
    private readonly ICartRepository _cartRepository;

    public UserSignedUpConsumer(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    
    public async Task Consume(ConsumeContext<UserSignedUp> context)
    {
        var message = context.Message;
        var cart = new Cart(message.Id);

        await _cartRepository.AddAsync(cart);
    }
}