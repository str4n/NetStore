using NetStore.Modules.Products.Core.Exceptions;
using NetStore.Modules.Products.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Time;

namespace NetStore.Modules.Products.Core.CQRS.Commands.Handlers;

internal sealed class UpdateDiscountHandler : ICommandHandler<UpdateDiscount>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IClock _clock;

    public UpdateDiscountHandler(IProductsRepository productsRepository, IClock clock)
    {
        _productsRepository = productsRepository;
        _clock = clock;
    }
    
    public async Task HandleAsync(UpdateDiscount command)
    {
        var product = await _productsRepository.GetAsync(command.Id, true);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        
        product.UpdateDiscount(command.Discount, _clock.Now());

        await _productsRepository.UpdateAsync(product);
    }
}