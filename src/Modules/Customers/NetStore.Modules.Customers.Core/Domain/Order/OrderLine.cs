namespace NetStore.Modules.Customers.Core.Domain.Order;

public sealed class OrderLine
{
    public Guid Id { get; set; }
    public int OrderLineNumber { get; set; }
    public string Name { get; set; }
    public double UnitPrice { get; set; }
    public int Quantity { get; set; }

    public OrderLine(Guid id, int orderLineNumber, string name, double unitPrice, int quantity)
    {
        Id = id;
        OrderLineNumber = orderLineNumber;
        Name = name;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    private OrderLine()
    {
    }
}