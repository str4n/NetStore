using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Product;
using NetStore.Modules.Orders.Domain.Repositories;

namespace NetStore.Modules.Orders.Tests.Unit.Helpers.Repositories;

internal sealed class InMemoryCartRepository : ICartRepository
{
    private static readonly List<Cart> Carts = new();

    public InMemoryCartRepository()
    {
        var cart = new Cart(Guid.Parse("00000000-0000-0000-0000-000000000001"));
        
        cart.AddProduct(new Product(Guid.NewGuid(), "mock", "mock", "mock", "mock", 999));

        AddAsync(cart);
    }

    public Task AddAsync(Cart cart)
    {
        Carts.Add(cart);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Cart cart)
    {
        return Task.CompletedTask;
    }

    public Task<Cart> GetByCustomerIdAsync(Guid customerId)
        => Task.FromResult(Carts.SingleOrDefault(x => x.CustomerId == customerId));
}