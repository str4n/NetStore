using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Orders.Shared.Enums;

namespace NetStore.Modules.Customers.Core.Domain.Order;

internal sealed class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public List<OrderLine> Lines { get; set; }
    public double Price => Lines.Sum(x => x.UnitPrice * x.Quantity);
    public OrderStatus Status { get; set; }
    public DateTime PlaceDate { get; set; }
    public Address.Address Address { get; set; }
    public string ReceiverName { get; set; }

    public Order(Guid id, Guid customerId ,IEnumerable<OrderLine> lines, OrderStatus status, DateTime placeDate, Address.Address address, string receiverName)
    {
        Id = id;
        CustomerId = customerId;
        Lines = lines.ToList();
        Status = status;
        PlaceDate = placeDate;
        Address = address;
        ReceiverName = receiverName;
    }

    private Order()
    {
    }
}