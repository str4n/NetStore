using NetStore.Modules.Catalogs.Application.Exceptions;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class DecreaseStockQuantityHandler : ICommandHandler<DecreaseStockQuantity>
{
    private readonly IProductRepository _productRepository;

    public DecreaseStockQuantityHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task HandleAsync(DecreaseStockQuantity command)
    {
        var product = await _productRepository.GetAsync(command.ProductId);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        
        product.DecreaseStock(command.Quantity);

        await _productRepository.UpdateAsync(product);
    }
}