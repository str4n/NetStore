using Microsoft.IdentityModel.Tokens;
using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class AddProductToCartHandler : ICommandHandler<AddProductToCart>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IIdentityContext _identityContext;

    public AddProductToCartHandler(ICartRepository cartRepository, IProductRepository productRepository, 
        IIdentityContext identityContext)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _identityContext = identityContext;
    }
    
    public async Task HandleAsync(AddProductToCart command)
    {
        var cart = await _cartRepository.GetByCustomerIdAsync(_identityContext.Id);
        var products = (await _productRepository.GetAvailableAsync(command.CodeName, command.Quantity)).ToList();
        
        if (cart is null)
        {
            throw new CartErrorException("Error occured while adding product to the cart.");
        }

        var currentCartProductCount =
            cart.Products.SingleOrDefault(x => x.Product.CodeName == command.CodeName)?.Quantity;

        var onStockCount = await _productRepository.GetAvailableCountAsync(command.CodeName);
        
        if (currentCartProductCount + command.Quantity > onStockCount)
        {
            throw new NotEnoughProductsOnStockException();
        }

        if (products.Count != command.Quantity)
        {
            throw new NotEnoughProductsOnStockException();
        }

        foreach (var product in products)
        {
            cart.AddProduct(product);
        }

        await _cartRepository.UpdateAsync(cart);
    }
}