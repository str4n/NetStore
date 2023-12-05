using NetStore.Modules.Orders.Domain.Exceptions;

namespace NetStore.Modules.Orders.Domain.Cart;

public sealed class CartProduct
{
    public Product.Product Product { get; private set; }
    public int Quantity { get; private set; }

    public CartProduct(Product.Product product, int quantity)
    {
        Product = product;
        if (quantity < 1)
        {
            throw new InvalidCartProductQuantityException();
        }

        Quantity = quantity;
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