using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Orders.Shared.Events;

public sealed record PaymentRequested(Guid PaymentId, Guid OrderId, Guid CustomerId, double Amount, DateTime DueDate) : IEvent;