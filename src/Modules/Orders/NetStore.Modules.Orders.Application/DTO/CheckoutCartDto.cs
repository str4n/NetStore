namespace NetStore.Modules.Orders.Application.DTO;

public sealed record CheckoutCartDto(string PaymentMethod, ShipmentDto Shipment, IEnumerable<CartProductDto> Products);