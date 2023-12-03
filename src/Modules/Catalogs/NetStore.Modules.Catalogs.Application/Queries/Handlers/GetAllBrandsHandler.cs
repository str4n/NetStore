using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Application.Queries.Handlers;

internal sealed class GetAllBrandsHandler : IQueryHandler<GetAllBrands, IEnumerable<BrandDto>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IEnumerable<BrandDto>> HandleAsync(GetAllBrands query)
        => (await _brandRepository.GetAll()).Select(x => new BrandDto(x.Id, x.Name));
}