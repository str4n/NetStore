using NetStore.Modules.Products.Core.DTO;

namespace NetStore.Modules.Products.Core.Services;

internal interface IProductsService
{
    Task AddAsync(ProductDto dto);
    Task DeleteAsync(Guid id);
    Task<ProductDto> GetAsync(Guid id);
    Task<IEnumerable<ProductDto>> GetAllAsync();
}