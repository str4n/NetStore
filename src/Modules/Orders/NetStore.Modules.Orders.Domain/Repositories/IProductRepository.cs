namespace NetStore.Modules.Orders.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product.Product>> GetAvailableAsync(string codeName, int quantity);
    Task<int> GetAvailableCountAsync(string codeName);
    Task AddAsync(Product.Product product);
}