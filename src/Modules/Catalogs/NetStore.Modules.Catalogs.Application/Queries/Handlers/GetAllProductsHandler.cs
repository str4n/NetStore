using System.Linq.Expressions;
using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Application.Mappings;
using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Application.Queries.Handlers;

internal sealed class GetAllProductsHandler : IQueryHandler<GetAllProducts, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IEnumerable<ProductDto>> HandleAsync(GetAllProducts query)
    {
        var products = (query.GetBy switch
        {
            "category" => await _productRepository.GetAllByCategoryAsync(query.Value, false),
            "brand" => await _productRepository.GetAllByBrandAsync(query.Value, false),
            _ => await _productRepository.GetAllAsync(false)
        }).ToList();


        return products.Select(x => x.AsDto());
    }
}