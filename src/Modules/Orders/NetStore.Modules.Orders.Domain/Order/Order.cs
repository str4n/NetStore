using NetStore.Modules.Orders.Domain.Cart;
using NetStore.Modules.Orders.Domain.Exceptions;
using NetStore.Shared.Types;
using NetStore.Shared.Types.Aggregate;

namespace NetStore.Modules.Orders.Domain.Order;

public sealed class Order : Aggregate
{
    public Guid CustomerId { get; private set; }
    public IEnumerable<OrderLine> Lines => _lines;
    private readonly List<OrderLine> _lines;
    public Payment.Payment Payment { get; private set; }
    public Shipment.Shipment Shipment { get; private set; }
    public DateTime PlaceDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private Order(Guid customerId, IEnumerable<OrderLine> orderLines, Shipment.Shipment shipment, Payment.Payment payment,
        DateTime placeDate)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        _lines = orderLines.ToList();
        Shipment = shipment;
        Payment = payment;
        PlaceDate = placeDate;
    }

    private Order()
    {
    }

    internal static Order CreateFromCheckout(CheckoutCart checkoutCart, DateTime placeDate)
    {
        foreach (var cartProduct in checkoutCart.Products)
        {
            cartProduct.Product.Order();
        }
        
        var orderLines = checkoutCart.Products.Select((x, i) =>
            new OrderLine(i, x.Product.Name, x.Product.SKU, x.Product.Price, x.Quantity)).ToList();
        
        var shipmentOrderLineNumber = orderLines.Max(x => x.OrderLineNumber) + 1;
        var shipmentLine = new OrderLine(shipmentOrderLineNumber, "Shipment","Shipment", 10, 1);

        orderLines.Add(shipmentLine);

        var order = new Order(checkoutCart.CustomerId, orderLines, checkoutCart.Shipment, checkoutCart.Payment,
            placeDate);
        
        order.ClearEvents();

        return order;
    }

    internal void Complete()
    {
        if (Status is OrderStatus.Canceled)
        {
            throw new CannotChangeOrderStatusException("Cannot change status of canceled order.");
        }

        Status = OrderStatus.Completed;
        IncrementVersion();
    }

    internal void Cancel()
    {
        if (Status is OrderStatus.Completed)
        {
            throw new CannotChangeOrderStatusException("Cannot change status of completed order.");
        }

        Status = OrderStatus.Canceled;
        IncrementVersion();
    }
    
    internal void Pay()
    {
        if (Status is not OrderStatus.Placed)
        {
            throw new CannotChangeOrderStatusException("Order is already paid.");
        }

        Status = OrderStatus.Paid;
        IncrementVersion();
    }

    internal void SetInProgress()
    {
        if (Status is not OrderStatus.Paid)
        {
            throw new CannotChangeOrderStatusException("Order must be paid.");
        }

        Status = OrderStatus.InProgress;
        IncrementVersion();
    }
}