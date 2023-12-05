namespace NetStore.Modules.Orders.Domain.Order;

public enum OrderStatus
{
    Placed,
    Paid,
    InProgress,
    Completed,
    Canceled,
}