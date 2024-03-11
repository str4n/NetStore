using MassTransit;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Commands;

namespace NetStore.Modules.Catalogs.Application.Messaging;

internal sealed class DecreaseStockConsumer : IConsumer<DecreaseStock>
{
    private readonly IProductRepository _productRepository;

    public DecreaseStockConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task Consume(ConsumeContext<DecreaseStock> context)
    {
        var (productId, amount) = context.Message;

        await _productRepository.DecreaseStockAsync(productId, amount);
    }
}