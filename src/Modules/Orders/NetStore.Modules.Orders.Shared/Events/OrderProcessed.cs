using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Orders.Shared.Events;

public sealed record OrderProcessed(Guid OrderId) : IEvent;