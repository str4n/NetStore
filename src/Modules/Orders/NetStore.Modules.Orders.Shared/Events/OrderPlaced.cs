using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Orders.Shared.Events;

public sealed record OrderPlaced(Guid OrderId, Guid CustomerId, Guid PaymentId, double Price) : IEvent;