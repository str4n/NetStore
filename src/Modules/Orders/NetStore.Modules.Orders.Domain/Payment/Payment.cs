namespace NetStore.Modules.Orders.Domain.Payment;

public sealed record Payment(Guid Id, PaymentMethod PaymentMethod, double Amount, string PaymentGatewaySecret, bool Payed);