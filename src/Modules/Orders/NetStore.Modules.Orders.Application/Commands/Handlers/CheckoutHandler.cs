using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class CheckoutHandler : ICommandHandler<Checkout>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IIdentityContext _identityContext;

    public CheckoutHandler(ICartRepository cartRepository, ICheckoutRepository checkoutRepository,
        IIdentityContext identityContext)
    {
        _cartRepository = cartRepository;
        _checkoutRepository = checkoutRepository;
        _identityContext = identityContext;
    }
    
    public async Task HandleAsync(Checkout command)
    {
        var customerId = _identityContext.Id;
        var cart = await _cartRepository.GetByCustomerIdAsync(customerId);
        
        if (cart is null)
        {
            throw new CartErrorException("Error occured while checking out the cart.");
        }
        
        var checkoutCart = cart.Checkout();
        
        var previousCheckoutCart = await _checkoutRepository.GetByCustomerId(customerId);

        if (previousCheckoutCart is not null)
        {
            previousCheckoutCart = checkoutCart;
            await _checkoutRepository.UpdateAsync(previousCheckoutCart);
            return;
        }

        await _checkoutRepository.AddAsync(checkoutCart);
    }
}