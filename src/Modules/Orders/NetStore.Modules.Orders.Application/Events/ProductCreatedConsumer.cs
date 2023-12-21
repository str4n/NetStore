using MassTransit;
using NetStore.Modules.Catalogs.Shared.Events;
using NetStore.Modules.Orders.Domain.Product;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Application.Events;

internal sealed class ProductCreatedConsumer : IConsumer<ProductCreated>
{
    private readonly IProductRepository _productRepository;

    public ProductCreatedConsumer(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        var message = context.Message;

        await _productRepository.AddAsync(new Product(message.Id, message.Name, message.SKU,message.Size, message.Color,
            message.Price));
    }
}