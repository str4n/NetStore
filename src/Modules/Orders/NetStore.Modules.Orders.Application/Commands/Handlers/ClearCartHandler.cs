using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class ClearCartHandler : ICommandHandler<ClearCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly IIdentityContext _identityContext;

    public ClearCartHandler(ICartRepository cartRepository, IIdentityContext identityContext)
    {
        _cartRepository = cartRepository;
        _identityContext = identityContext;
    }
    public async Task HandleAsync(ClearCart command)
    {
        var cart = await _cartRepository.GetByCustomerIdAsync(_identityContext.Id);
        
        if (cart is null)
        {
            throw new CartErrorException("Error occured while clearing cart.");
        }
        
        cart.Clear();

        await _cartRepository.UpdateAsync(cart);
    }
}