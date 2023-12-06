using NetStore.Modules.Catalogs.Shared.Events;
using NetStore.Modules.Orders.Domain.Product;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Orders.Application.Events.Handlers;

internal sealed class ProductCreatedHandler : IEventHandler<ProductCreated>
{
    private readonly IProductRepository _productRepository;

    public ProductCreatedHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }   
    public async Task HandleAsync(ProductCreated @event)
        => await _productRepository.AddAsync(new Product(@event.Id, @event.Name, @event.SKU, @event.Price));
}