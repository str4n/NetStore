namespace NetStore.Modules.Orders.Application.DTO;

public sealed record CartDto(IEnumerable<CartProductDto> Products);