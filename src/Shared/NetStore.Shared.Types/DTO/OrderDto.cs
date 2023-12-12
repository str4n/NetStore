namespace NetStore.Shared.Types.DTO;

public sealed record OrderDto(Guid OrderId, string City, string Street, string PostalCode, string ReceiverName, DateTime PlaceDate, IEnumerable<ProductDto> Products);