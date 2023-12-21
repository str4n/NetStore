using MassTransit;
using NetStore.Modules.Catalogs.Shared.Events;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Application.Events;

internal sealed class ProductStockQuantityDecreasedConsumer : IConsumer<ProductStockQuantityDecreased>
{
    private readonly IProductRepository _productRepository;

    public ProductStockQuantityDecreasedConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task Consume(ConsumeContext<ProductStockQuantityDecreased> context)
    {
        var product = await _productRepository.GetAsync(context.Message.ProductId);

        product.Stock -= context.Message.Quantity;

        await _productRepository.UpdateAsync(product);
    }
}