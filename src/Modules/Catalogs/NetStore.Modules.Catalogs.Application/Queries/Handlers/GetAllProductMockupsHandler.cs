using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Application.Mappings;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Application.Queries.Handlers;

internal sealed class GetAllProductMockupsHandler : IQueryHandler<GetAllProductMockups, IEnumerable<ProductMockupDto>>
{
    private readonly IProductMockupRepository _productMockupRepository;

    public GetAllProductMockupsHandler(IProductMockupRepository productMockupRepository)
    {
        _productMockupRepository = productMockupRepository;
    }

    public async Task<IEnumerable<ProductMockupDto>> HandleAsync(GetAllProductMockups query)
        => (await _productMockupRepository.GetAllAsync()).Select(x => x.AsDto());
}