namespace NetStore.Shared.Types.DTO;

public sealed record SetupPaymentDto(Guid PaymentId, Guid CustomerId, double Amount, DateTime DueDate);