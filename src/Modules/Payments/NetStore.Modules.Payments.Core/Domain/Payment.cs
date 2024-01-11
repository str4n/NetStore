namespace NetStore.Modules.Payments.Core.Domain;

public sealed record Payment(Guid PaymentId, Guid CustomerId, double Amount, DateTime DueDate, string PaymentGatewaySecret, bool Payed);