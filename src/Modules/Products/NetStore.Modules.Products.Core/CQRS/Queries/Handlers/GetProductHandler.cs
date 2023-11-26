using NetStore.Modules.Products.Core.DTO;
using NetStore.Modules.Products.Core.Exceptions;
using NetStore.Modules.Products.Core.Mappings;
using NetStore.Modules.Products.Core.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Products.Core.CQRS.Queries.Handlers;

internal sealed class GetProductHandler : IQueryHandler<GetProduct, ProductDto>
{
    private readonly IProductsRepository _productsRepository;

    public GetProductHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }
    
    public async Task<ProductDto> HandleAsync(GetProduct query)
    {
        var product = await _productsRepository.GetAsync(query.Id);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return product.AsDto();
    }
}