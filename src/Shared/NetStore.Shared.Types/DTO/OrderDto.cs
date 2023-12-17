namespace NetStore.Shared.Types.DTO;

public sealed record OrderDto(Guid Id, string City, string Street, string PostalCode, string ReceiverName, DateTime PlaceDate, IEnumerable<OrderLineDto> Lines);