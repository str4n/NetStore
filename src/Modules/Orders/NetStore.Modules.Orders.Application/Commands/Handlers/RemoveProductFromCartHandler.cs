using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class RemoveProductFromCartHandler : ICommandHandler<RemoveProductFromCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly IIdentityContext _identityContext;

    public RemoveProductFromCartHandler(ICartRepository cartRepository, IIdentityContext identityContext)
    {
        _cartRepository = cartRepository;
        _identityContext = identityContext;
    }
    public async Task HandleAsync(RemoveProductFromCart command)
    {
        var cart = await _cartRepository.GetByCustomerIdAsync(_identityContext.Id);
        
        if (cart is null)
        {
            throw new CartErrorException("Error occured while removing product from the cart.");
        }

        var productsToRemove = cart.Products
            .Where(x => x.Product.CodeName == command.CodeName)
            .Take(command.Quantity)
            .Select(x => x.Product)
            .ToList();

        if (productsToRemove.Count != command.Quantity)
        {
            throw new NotEnoughProductsInCartException(command.Quantity);
        }

        foreach (var product in productsToRemove)
        {
            cart.RemoveProduct(product);
        }

        await _cartRepository.UpdateAsync(cart);
    }
}