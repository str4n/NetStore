using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class ClearCartHandler : ICommandHandler<ClearCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly IIdentityContext _identityContext;
    private readonly ICheckoutRepository _checkoutRepository;

    public ClearCartHandler(ICartRepository cartRepository, IIdentityContext identityContext, 
        ICheckoutRepository checkoutRepository)
    {
        _cartRepository = cartRepository;
        _identityContext = identityContext;
        _checkoutRepository = checkoutRepository;
    }
    public async Task HandleAsync(ClearCart command)
    {
        var customerId = _identityContext.Id;
        var cart = await _cartRepository.GetByCustomerIdAsync(customerId);
        
        if (cart is null)
        {
            throw new CartErrorException("Error occured while clearing cart.");
        }
        
        cart.Clear();
        
        await _cartRepository.UpdateAsync(cart);
    }
}