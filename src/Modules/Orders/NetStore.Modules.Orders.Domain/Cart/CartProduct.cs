using NetStore.Modules.Orders.Domain.Exceptions;

namespace NetStore.Modules.Orders.Domain.Cart;

public sealed class CartProduct
{
    public Guid CartId { get; private set; }
    public Product.Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public CartProduct(Guid cartId, Product.Product product, int quantity)
    {
        Product = product;
        if (quantity < 1)
        {
            throw new InvalidCartProductQuantityException();
        }

        Quantity = quantity;
        CartId = cartId;
    }

    private CartProduct()
    {
    }

    public void IncreaseQuantity() => Quantity++;
    public void DecreaseQuantity()
    {
        if (Quantity - 1 < 0)
        {
            throw new InvalidCartProductQuantityException();
        }

        Quantity--;
    }
}