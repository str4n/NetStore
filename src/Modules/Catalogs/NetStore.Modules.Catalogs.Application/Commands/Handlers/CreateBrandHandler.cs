using NetStore.Modules.Catalogs.Domain.Brand;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class CreateBrandHandler : ICommandHandler<CreateBrand>
{
    private readonly IBrandRepository _brandRepository;

    public CreateBrandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    
    public async Task HandleAsync(CreateBrand command)
    {
        var brand = Brand.Create(command.Name);

        await _brandRepository.AddAsync(brand);
    }
}