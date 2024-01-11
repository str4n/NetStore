using NetStore.Modules.Orders.Shared.DTO;
using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Orders.Shared.Events;

public sealed record OrderPlaced(Guid CustomerId, OrderDto Order) : IEvent;