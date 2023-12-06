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

    public Task Consume(ConsumeContext<ProductCreated> context)
        => _productRepository.AddAsync(new Product(context.Message.Id, context.Message.Name, context.Message.SKU,
            context.Message.Price));
}