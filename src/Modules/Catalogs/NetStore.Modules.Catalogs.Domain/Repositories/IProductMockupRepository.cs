using NetStore.Modules.Catalogs.Domain.Product.Mockup;

namespace NetStore.Modules.Catalogs.Domain.Repositories;

public interface IProductMockupRepository
{
    Task<ProductMockup> GetAsync(long id);
    Task<IEnumerable<ProductMockup>> GetAllAsync();
    Task AddAsync(ProductMockup mockup);
    Task UpdateAsync(ProductMockup mockup);
}