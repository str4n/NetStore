using NetStore.Modules.Products.Core.Domain.Entities;
using NetStore.Modules.Products.Core.Domain.ValueObjects;
using NetStore.Modules.Products.Core.Exceptions;
using NetStore.Modules.Products.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Time;

namespace NetStore.Modules.Products.Core.CQRS.Commands.Handlers;

internal sealed class CreateProductHandler : ICommandHandler<CreateProduct>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IClock _clock;

    public CreateProductHandler(IProductsRepository productsRepository, IClock clock)
    {
        _productsRepository = productsRepository;
        _clock = clock;
    }
    
    public async Task HandleAsync(CreateProduct command)
    {
        if (command is null) throw new ProductNullException();

        var product = Product.Create(command.Id, command.Name, command.Description, command.Categories.Select(x => (Category)x), command.Price, _clock.Now() ,
            command.Discount);

        await _productsRepository.AddAsync(product);
    }
}