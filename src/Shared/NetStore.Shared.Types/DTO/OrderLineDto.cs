namespace NetStore.Shared.Types.DTO;

public sealed record OrderLineDto(int OrderLineNumber, string Name, int Quantity, double UnitPrice);