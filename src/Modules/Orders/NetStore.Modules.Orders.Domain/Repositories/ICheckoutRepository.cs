using NetStore.Modules.Orders.Domain.Cart;

namespace NetStore.Modules.Orders.Domain.Repositories;

public interface ICheckoutRepository
{
    Task<CheckoutCart> GetAsync(Guid id);
    Task<CheckoutCart> GetByCustomerId(Guid id);
    Task UpdateAsync(CheckoutCart checkoutCart);
    Task AddAsync(CheckoutCart checkoutCart);
}   