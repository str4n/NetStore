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
        
        // Decrease stock
    }
}