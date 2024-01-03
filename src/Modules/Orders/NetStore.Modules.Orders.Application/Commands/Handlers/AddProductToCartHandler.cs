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
        
        if (cart is null)
        {
            throw new CartErrorException("Error occured while adding product to the cart.");
        }

        var product = await _productRepository.GetAsync(command.Id);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        
        if (command.Quantity > product.Stock)
        {
            throw new NotEnoughProductsOnStockException();
        }

        var cartProduct = cart.Products.SingleOrDefault(x => x.Product.Id == product.Id);

        if (cartProduct?.Quantity + command.Quantity > product.Stock)
        {
            throw new NotEnoughProductsOnStockException();
        }

        for (int i = 0; i < command.Quantity; i++)
        {
            cart.AddProduct(product);
        }
        
        await _cartRepository.UpdateAsync(cart);
    }
}