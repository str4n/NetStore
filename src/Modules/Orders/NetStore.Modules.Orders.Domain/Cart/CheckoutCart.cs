using NetStore.Modules.Orders.Domain.Payment;

namespace NetStore.Modules.Orders.Domain.Cart;

public sealed class CheckoutCart
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public PaymentCard PaymentCard { get; private set; }
    public Shipment.Shipment Shipment { get; private set; }
    public IEnumerable<CartProduct> Products => _products;
    private readonly List<CartProduct> _products;

    internal CheckoutCart(Cart cart)
    {
        Id = Guid.NewGuid();
        CustomerId = cart.CustomerId;
        _products = cart.Products.ToList();
    }

    public void SetShipment(Shipment.Shipment shipment) => Shipment = shipment;
    public void SetPaymentCard(PaymentCard paymentCard) => PaymentCard = paymentCard;

    public Order.Order PlaceOrder(DateTime placeDate) => Order.Order.CreateFromCheckout(this, placeDate);
}