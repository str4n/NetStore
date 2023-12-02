namespace NetStore.Modules.Catalogs.Domain.Repositories;

public interface IProductRepository
{
    Task<Product.Product> GetAsync(Guid id);
    Task<IEnumerable<Product.Product>> GetAllAsync();
    Task AddAsync(IEnumerable<Product.Product> products);
    Task UpdateAsync(Product.Product product);
}