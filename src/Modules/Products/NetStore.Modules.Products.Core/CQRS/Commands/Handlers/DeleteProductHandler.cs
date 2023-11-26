using NetStore.Modules.Products.Core.Exceptions;
using NetStore.Modules.Products.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Products.Core.CQRS.Commands.Handlers;

internal sealed class DeleteProductHandler : ICommandHandler<DeleteProduct>
{
    private readonly IProductsRepository _productsRepository;

    public DeleteProductHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    
    public async Task HandleAsync(DeleteProduct command)
    {
        var product = await _productsRepository.GetAsync(command.Id, true);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        await _productsRepository.DeleteAsync(product);
    }
}