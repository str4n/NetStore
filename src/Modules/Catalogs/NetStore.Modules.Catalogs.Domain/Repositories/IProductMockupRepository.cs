namespace NetStore.Modules.Catalogs.Domain.Repositories;

public interface IProductMockupRepository
{
    Task<Product.Product> GetAsync(Guid id);
    Task AddAsync(Product.Product product);
    Task UpdateAsync(Product.Product product);
}