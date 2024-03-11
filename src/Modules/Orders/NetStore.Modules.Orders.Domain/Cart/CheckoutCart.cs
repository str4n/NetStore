using NetStore.Modules.Orders.Domain.Exceptions;
using NetStore.Modules.Orders.Domain.Payment;

namespace NetStore.Modules.Orders.Domain.Cart;

public sealed class CheckoutCart
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Payment.Payment Payment { get; private set; }
    public Shipment.Shipment Shipment { get; private set; }
    public IEnumerable<CartProduct> Products => _products;
    private readonly List<CartProduct> _products;
    
    public CheckoutCart(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
    }

    internal CheckoutCart(Cart cart)
    {
        Id = Guid.NewGuid();
        CustomerId = cart.CustomerId;
        _products = cart.Products.ToList();
    }

    private CheckoutCart()
    {
    }

    public void SetShipment(Shipment.Shipment shipment) => Shipment = shipment;
    public void SetPayment(Payment.Payment payment) => Payment = payment;
    public bool IsInformationCompleted() => Shipment is not null && Payment is not null && Products.Any();

    public void Clear()
    {
        Payment = null;
        Shipment = null;
        _products.Clear();
    }

    public Order.Order PlaceOrder(DateTime placeDate)
    {
        if (!IsInformationCompleted())
        {
            throw new CheckoutNotCompletedException();
        }

        return Order.Order.CreateFromCheckout(this, placeDate);
    }
}