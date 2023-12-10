using NetStore.Modules.Orders.Domain.Cart;

namespace NetStore.Modules.Orders.Domain.Repositories;

public interface ICheckoutRepository
{
    Task<CheckoutCart> GetAsync(Guid id);
    Task AddAsync(CheckoutCart checkoutCart);
}   