using NetStore.Modules.Products.Core.Domain.Entities;
using NetStore.Modules.Products.Core.DTO;
using NetStore.Modules.Products.Core.Exceptions;
using NetStore.Modules.Products.Core.Mappings;
using NetStore.Modules.Products.Core.Repositories;

namespace NetStore.Modules.Products.Core.Services;

internal sealed class ProductsService : IProductsService
{
    private readonly IProductsRepository _repository;

    public ProductsService(IProductsRepository repository)
    {
        _repository = repository;
    }
    
    public async Task AddAsync(ProductDto dto)
    {
        if (dto is null) throw new ProductNullException();

        var product = dto.ToEntity();

        await _repository.AddAsync(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetAsync(id, true);

        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }

        await _repository.DeleteAsync(product);
    }

    public async Task<ProductDto> GetAsync(Guid id)
    {
        var product = await _repository.GetAsync(id);

        if (product is null)
        {
            throw new ProductNotFoundException(id);
        }

        return product.AsDto();
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();

        if (products is null)
        {
            return Enumerable.Empty<ProductDto>();
        }

        return products.Select(x => x.AsDto());
    }
}