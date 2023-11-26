using NetStore.Modules.Products.Core.DTO;
using NetStore.Modules.Products.Core.Mappings;
using NetStore.Modules.Products.Core.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Products.Core.CQRS.Queries.Handlers;

internal sealed class GetAllProductsHandler : IQueryHandler<GetAllProducts, IEnumerable<ProductDto>>
{
    private readonly IProductsRepository _productsRepository;

    public GetAllProductsHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    
    public async Task<IEnumerable<ProductDto>> HandleAsync(GetAllProducts query)
    {
        var products = await _productsRepository.GetAllAsync();

        if (products is null)
        {
            return Enumerable.Empty<ProductDto>();
        }

        return products.Select(x => x.AsDto());
    }
}