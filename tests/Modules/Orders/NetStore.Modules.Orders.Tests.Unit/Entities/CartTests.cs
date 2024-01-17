using FluentAssertions;
using Moq;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Exceptions;
using NetStore.Modules.Orders.Domain.Product;
using Xunit;

namespace NetStore.Modules.Orders.Tests.Unit.Entities;

public class CartTests
{
    [Fact]
    public void Add_Product_To_Cart_Should_Succeed()
    {
        var cart = new Cart(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "mock","mock","mock","mock",999);
        
        cart.AddProduct(product);

        cart.Products.Should().NotBeEmpty();
        cart.Products.Should().ContainSingle();
    }
    
    [Fact]
    public void Add_Many_Products_Of_Same_Type_To_Cart_Should_Succeed()
    {
        var cart = new Cart(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "mock","mock","mock","mock",999);
        
        cart.AddProduct(product);
        cart.AddProduct(product);

        cart.Products.Should().NotBeEmpty();
        cart.Products.Should().ContainSingle();
        cart.Products.First().Quantity.Should().Be(2);
    }
    
    [Fact]
    public void Add_Many_Products_Of_Different_Type_To_Cart_Should_Succeed()
    {
        var cart = new Cart(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "mock","mock","mock","mock",999);
        var product2 = new Product(Guid.NewGuid(), "mock2","mock2","mock2","mock2",1000);
        
        cart.AddProduct(product);
        cart.AddProduct(product2);

        cart.Products.Should().NotBeEmpty();
        cart.Products.Should().HaveCount(2);
    }

    [Fact]
    public void Remove_Last_Product_From_Cart_Should_Succeed()
    {
        var cart = new Cart(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "mock","mock","mock","mock",999);
        
        cart.AddProduct(product);
        
        cart.RemoveProduct(product);

        cart.Products.Should().BeEmpty();
    }

    [Fact]
    public void Remove_Products_Of_Same_Type_From_Cart_Should_Succeed()
    {
        var cart = new Cart(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "mock","mock","mock","mock",999);
        
        cart.AddProduct(product);
        cart.AddProduct(product);
        
        cart.RemoveProduct(product);

        cart.Products.First().Quantity.Should().Be(1);
    }

    [Fact]
    public void Clear_Cart_Should_Succeed()
    {
        var cart = new Cart(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "mock","mock","mock","mock",999);
        
        cart.AddProduct(product);
        
        cart.Clear();
        
        cart.Products.Should().BeEmpty();
    }

    [Fact]
    public void Cart_Checkout_Should_Return_Proper_CheckoutCart()
    {
        var cart = new Cart(Guid.NewGuid());
        var product = new Product(Guid.NewGuid(), "mock","mock","mock","mock",999);
        
        cart.AddProduct(product);

        var checkout = cart.Checkout();

        checkout.Should().NotBeNull();
        checkout.Should().BeOfType<CheckoutCart>();
        checkout.Products.Should().ContainSingle();
    }

    [Fact]
    public void Empty_Cart_Checkout_Should_Throw_Exception()
    {
        var cart = new Cart(Guid.NewGuid());

        var act = () => cart.Checkout();

        act.Should().Throw<CannotCheckoutEmptyCartException>();
    }
}