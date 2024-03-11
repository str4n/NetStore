using MassTransit;
using NetStore.Modules.Catalogs.Shared.Events;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Application.Messaging;

internal sealed class ProductStockQuantityIncreasedConsumer : IConsumer<ProductStockQuantityIncreased>
{
    private readonly IProductRepository _productRepository;

    public ProductStockQuantityIncreasedConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task Consume(ConsumeContext<ProductStockQuantityIncreased> context)
    {
        var product = await _productRepository.GetAsync(context.Message.ProductId);

        product.Stock += context.Message.Quantity;
        
        await _productRepository.UpdateAsync(product);
    }
}