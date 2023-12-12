using NetStore.Shared.Abstractions.Events;
using NetStore.Shared.Types.DTO;

namespace NetStore.Modules.Orders.Shared.Events;

public sealed record OrderPlaced(Guid CustomerId, OrderDto Order) : IEvent;