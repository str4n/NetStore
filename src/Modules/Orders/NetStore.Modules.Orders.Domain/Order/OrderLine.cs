using NetStore.Modules.Orders.Domain.Exceptions;

namespace NetStore.Modules.Orders.Domain.Order;

public sealed class OrderLine
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public int OrderLineNumber { get; private set; }
    public string Name { get; private set; }
    public string SKU { get; private set; }
    public double UnitPrice { get; }
    public int Quantity { get; }
    
    public OrderLine(Guid productId, int orderLineNumber, string name, string sku,  double unitPrice, int quantity)
    {
        if (quantity < 1)
        {
            throw new InvalidOrderLineDataException("Order line quantity must be greater than 0.");
        }
        if (unitPrice < 0)
        {
            throw new InvalidOrderLineDataException("Unit price cannot be negative or zero.");
        }

        Id = Guid.NewGuid();
        ProductId = productId;
        OrderLineNumber = orderLineNumber;
        SKU = sku;
        Name = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    private OrderLine()
    {
    }
}