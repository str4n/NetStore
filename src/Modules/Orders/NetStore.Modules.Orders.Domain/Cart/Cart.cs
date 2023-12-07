﻿using NetStore.Modules.Orders.Domain.Exceptions;
using NetStore.Modules.Orders.Domain.Product;

namespace NetStore.Modules.Orders.Domain.Cart;

public sealed class Cart
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public IEnumerable<CartProduct> Products => _products;
    private List<CartProduct> _products = new();

    public Cart(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
    }

    public void AddProduct(Product.Product product)
    {
        var cartProduct = _products.SingleOrDefault(x => x.Product.Id == product.Id);

        if (product.State == ProductState.Locked)
        {
            throw new ProductAlreadyInDifferentCartException(product.Id);
        }
        
        product.LockProduct();

        if (cartProduct is null)
        {
            _products.Add(new CartProduct(Id, product,1));
            return;
        }
        
        cartProduct.IncreaseQuantity();
    }

    public void RemoveProduct(Product.Product product)
    {
        var cartProduct = _products.SingleOrDefault(x => x.Product.Id == product.Id);

        if (cartProduct is null)
        {
            throw new CartProductNotFoundException();
        }
        
        product.UnlockProduct();

        if (cartProduct.Quantity is 1)
        {
            _products.Remove(cartProduct);
            return;
        }
        
        cartProduct.DecreaseQuantity();
    }

    public void Clear()
    {
        foreach (var product in _products)
        {
            product.Product.UnlockProduct();
        }
        
        _products.Clear();
    }

    public CheckoutCart CheckoutCart()
    {
        if (_products.Count is 0)
        {
            throw new CannotCheckoutEmptyCartException();
        }

        return new CheckoutCart(this);
    }
}