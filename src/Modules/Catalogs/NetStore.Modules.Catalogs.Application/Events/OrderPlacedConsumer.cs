using MassTransit;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Events;

namespace NetStore.Modules.Catalogs.Application.Events;

internal sealed class OrderPlacedConsumer : IConsumer<OrderPlaced>
{
    private readonly IProductRepository _productRepository;

    public OrderPlacedConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        var message = context.Message;
        var orderLines = message.Order.Lines;

        foreach (var line in orderLines)
        {
            await _productRepository.DecreaseStockAsync(line.ProductId, line.Quantity);
        }

        await _productRepository.SaveChangesAsync();
    }
}