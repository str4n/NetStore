using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Exceptions;
using NetStore.Modules.Orders.Domain.Payment;

namespace NetStore.Modules.Orders.Domain.Order;

public sealed class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public IEnumerable<OrderLine> OrderLines => _orderLines;
    private readonly List<OrderLine> _orderLines;
    public PaymentCard PaymentCard { get; private set; }
    public Shipment.Shipment Shipment { get; private set; }
    public DateTime PlaceDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private Order(Guid customerId, IEnumerable<OrderLine> orderLines, Shipment.Shipment shipment, PaymentCard paymentCard,
        DateTime placeDate)
    {
        CustomerId = customerId;
        _orderLines = orderLines.ToList();
        Shipment = shipment;
        PaymentCard = paymentCard;
        PlaceDate = placeDate;
    }

    internal static Order CreateFromCheckout(CheckoutCart checkoutCart, DateTime placeDate)
    {
        var orderLines = checkoutCart.Products.Select((x, i) =>
            new OrderLine(i, x.Product.Name, x.Product.SKU, x.Product.Price, x.Quantity)).ToList();
        
        var shipmentOrderLineNumber = orderLines.Max(x => x.OrderLineNumber) + 1;
        var shipmentLine = new OrderLine(shipmentOrderLineNumber, "Shipment","Shipment", 10, 1);

        orderLines.Add(shipmentLine);

        return new Order(checkoutCart.CustomerId, orderLines, checkoutCart.Shipment, checkoutCart.PaymentCard,
            placeDate);
    }

    internal void Complete()
    {
        if (Status is OrderStatus.Canceled)
        {
            throw new CannotChangeOrderStatusException("Cannot change status of canceled order.");
        }

        Status = OrderStatus.Completed;
    }

    internal void Cancel()
    {
        if (Status is OrderStatus.Completed)
        {
            throw new CannotChangeOrderStatusException("Cannot change status of completed order.");
        }

        Status = OrderStatus.Canceled;
    }
    
    internal void Pay()
    {
        if (Status is not OrderStatus.Placed)
        {
            throw new CannotChangeOrderStatusException("Order is already paid.");
        }

        Status = OrderStatus.Paid;
    }

    internal void SetInProgress()
    {
        if (Status is not OrderStatus.Paid)
        {
            throw new CannotChangeOrderStatusException("Order must be paid.");
        }

        Status = OrderStatus.InProgress;
    }
}