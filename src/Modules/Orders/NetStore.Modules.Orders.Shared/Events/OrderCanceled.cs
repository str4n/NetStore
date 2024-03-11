using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Orders.Shared.Events;

public sealed record OrderCanceled(Guid OrderId) : IEvent;