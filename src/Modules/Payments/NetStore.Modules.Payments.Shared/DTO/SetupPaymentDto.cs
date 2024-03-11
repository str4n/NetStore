namespace NetStore.Modules.Payments.Shared.DTO;

public sealed record SetupPaymentDto(Guid PaymentId, Guid CustomerId, double Amount);