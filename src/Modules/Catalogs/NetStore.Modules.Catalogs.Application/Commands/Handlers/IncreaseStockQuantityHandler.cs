using NetStore.Modules.Catalogs.Application.Exceptions;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class IncreaseStockQuantityHandler : ICommandHandler<IncreaseStockQuantity>
{
    private readonly IProductRepository _productRepository;

    public IncreaseStockQuantityHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
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
    }
}