namespace NetStore.Modules.Orders.Domain.Repositories;

public interface ICartRepository
{
    Task AddAsync(Cart.Cart cart);
    Task UpdateAsync(Cart.Cart cart);
    Task<Cart.Cart> GetByCustomerIdAsync(Guid customerId);
}