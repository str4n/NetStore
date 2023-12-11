namespace NetStore.Modules.Orders.Application.DTO;

public sealed record ShipmentDto(string City, string Street, string PostalCode, string ReceiverName);