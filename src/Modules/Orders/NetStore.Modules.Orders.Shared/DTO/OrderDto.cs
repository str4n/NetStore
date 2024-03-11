using NetStore.Modules.Orders.Shared.Enums;

namespace NetStore.Modules.Orders.Shared.DTO;

public sealed record OrderDto(Guid Id, Guid CustomerId ,string ReceiverName, string City, string Street, string PostalCode, double Price,
    OrderStatus Status,
    DateTime PlaceDate, IEnumerable<OrderLineDto> Lines);