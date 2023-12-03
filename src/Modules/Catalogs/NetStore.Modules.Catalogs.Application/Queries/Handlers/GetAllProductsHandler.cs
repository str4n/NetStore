using System.Linq.Expressions;
using NetStore.Modules.Catalogs.Application.DTO;
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
        var products = query.GetBy switch
        {
            "category" => await _productRepository.GetAllByCategoryAsync(query.Value, false),
            "brand" => await _productRepository.GetAllByBrandAsync(query.Value, false),
            _ => await _productRepository.GetAllAsync(false)
        };

        products = products
            .GroupBy(p => new { p.Size, p.Color, p.Name })
            .Select(g => g.First())
            .ToList();

        var dtos = products.Select(x => 
            new ProductDto(x.Id, x.Name, x.Description, x.Category.Name, x.Brand.Name, 
                x.Model, x.GrossPrice, x.Fabric, 
                x.Gender.ToString(),x.AgeCategory.ToString(), x.Size.ToString(), x.Color.ToString()));

        return dtos;
    }
}