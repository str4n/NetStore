using NetStore.Modules.Catalogs.Application.Exceptions;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Modules.Catalogs.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class IncreaseStockQuantityHandler : ICommandHandler<IncreaseStockQuantity>
{
    private readonly IProductRepository _productRepository;
    private readonly IMessageBroker _messageBroker;

    public IncreaseStockQuantityHandler(IProductRepository productRepository, IMessageBroker messageBroker)
    {
        _productRepository = productRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(IncreaseStockQuantity command)
    {
        var product = await _productRepository.GetAsync(command.ProductId);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        
        product.IncreaseStock(command.Quantity);

        await _productRepository.UpdateAsync(product);
        
        await _messageBroker.PublishAsync(new ProductStockQuantityIncreased(command.ProductId, command.Quantity));
    }
}