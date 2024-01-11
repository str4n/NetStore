namespace FakePaymentGateway.DTO;

public sealed record PaymentWebhookDto(Guid PaymentId, string PaymentGatewaySecret);