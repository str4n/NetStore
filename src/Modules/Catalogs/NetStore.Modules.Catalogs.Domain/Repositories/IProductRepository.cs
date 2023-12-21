using System.Linq.Expressions;

namespace NetStore.Modules.Catalogs.Domain.Repositories;

public interface IProductRepository
{
    Task<Product.Product> GetAsync(Guid id);
    Task<IEnumerable<Product.Product>> GetAllAsync(bool tracking = true);
    Task<IEnumerable<Product.Product>> GetAllByCategoryAsync(string category, bool tracking = true);
    Task<IEnumerable<Product.Product>> GetAllByBrandAsync(string brand, bool tracking = true);
    Task AddAsync(Product.Product product);
    Task UpdateAsync(Product.Product product);
}