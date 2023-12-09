namespace NetStore.Modules.Orders.Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product.Product>> GetAvailableAsync(string productName, int quantity);
    Task AddAsync(Product.Product product);
}