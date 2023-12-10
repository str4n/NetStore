namespace NetStore.Modules.Orders.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product.Product>> GetAvailableAsync(string codeName, int quantity);
    Task AddAsync(Product.Product product);
}