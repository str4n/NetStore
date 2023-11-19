using NetStore.Modules.Products.Core.Domain.Entities;

namespace NetStore.Modules.Products.Core.Repositories;

internal interface IProductsRepository
{
    Task AddAsync(Product product);
    Task DeleteAsync(Product product);
    Task UpdateAsync(Product product);
    Task<Product> GetAsync(Guid id, bool tracking = false);
    Task<IEnumerable<Product>> GetAllAsync(bool tracking = false);
}