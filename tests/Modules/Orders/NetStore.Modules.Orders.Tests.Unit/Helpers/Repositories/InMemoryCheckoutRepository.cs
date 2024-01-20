using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Tests.Unit.Helpers.Repositories;

internal sealed class InMemoryCheckoutRepository : ICheckoutRepository
{
    private static readonly List<CheckoutCart> CheckoutCarts = new();

    public Task<CheckoutCart> GetAsync(Guid id)
        => Task.FromResult(CheckoutCarts.SingleOrDefault(x => x.Id == id));

    public Task<CheckoutCart> GetByCustomerId(Guid id)
        => Task.FromResult(CheckoutCarts.SingleOrDefault(x => x.CustomerId == id));

    public Task UpdateAsync(CheckoutCart checkoutCart)
    {
        return Task.CompletedTask;
    }

    public Task AddAsync(CheckoutCart checkoutCart)
    {
        CheckoutCarts.Add(checkoutCart);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid customerId)
    {
        CheckoutCarts.Remove(CheckoutCarts.SingleOrDefault(x => x.CustomerId == customerId));

        return Task.CompletedTask;
    }
}