using MassTransit;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Events;

namespace NetStore.Modules.Orders.Application.Events;

internal sealed class OrderPlacedConsumer : IConsumer<OrderPlaced>
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public OrderPlacedConsumer(ICheckoutRepository checkoutRepository, ICartRepository cartRepository, 
        IProductRepository productRepository)
    {
        _checkoutRepository = checkoutRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }
    
    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        var message = context.Message;

        var cart = await _cartRepository.GetByCustomerIdAsync(message.CustomerId);

        var cartProducts = cart.Products.ToList();

        foreach (var cartProduct in cartProducts)
        {
            await _productRepository.DecreaseStockAsync(cartProduct.ProductId, cartProduct.Quantity);
        }
        
        cart.Clear();

        await _cartRepository.UpdateAsync(cart);

        await _checkoutRepository.DeleteAsync(message.CustomerId);
    }
}