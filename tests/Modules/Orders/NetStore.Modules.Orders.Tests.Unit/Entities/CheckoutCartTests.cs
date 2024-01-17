using FluentAssertions;
using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Order;
using NetStore.Modules.Orders.Domain.Payment;
using NetStore.Modules.Orders.Domain.Product;
using NetStore.Modules.Orders.Domain.Shipment;
using Xunit;

namespace NetStore.Modules.Orders.Tests.Unit.Entities;

public class CheckoutCartTests
{
    [Fact]
    public void Place_Order_From_Valid_Checkout_Should_Return_Proper_Order()
    {
        var cart = new Cart(Guid.NewGuid());
        var price = 999;
        var product = new Product(Guid.NewGuid(), "mock","mock","mock","mock",price);
        
        cart.AddProduct(product);
        
        var checkoutCart = new CheckoutCart(cart);

        var shipment = new Shipment("mock", "mock", "mock", "mock");
        
        checkoutCart.SetShipment(shipment);

        var payment = new Payment(Guid.NewGuid(), PaymentMethod.BankTransfer, price, "", false);
        
        checkoutCart.SetPayment(payment);

        var order = checkoutCart.PlaceOrder(DateTime.Now);

        order.Should().NotBeNull();
        order.Should().BeOfType<Order>();
        order.Lines.Should().NotBeEmpty();

        // w/shipment price, yes its hard coded
        // TODO: shipment price
        order.Payment.Should().BeEquivalentTo(payment with { Amount = price + 10 });
        order.Shipment.Should().BeEquivalentTo(shipment);
    }
}