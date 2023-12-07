namespace NetStore.Modules.Orders.Domain.Repositories;

public interface ICartRepository
{
    Task AddAsync(Cart.Cart cart);
    Task<Cart.Cart> GetAsync(Guid customerId);
}