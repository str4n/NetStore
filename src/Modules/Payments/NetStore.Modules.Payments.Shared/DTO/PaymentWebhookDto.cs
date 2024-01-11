namespace NetStore.Modules.Payments.Shared.DTO;

public sealed record PaymentWebhookDto(Guid PaymentId, string PaymentGatewaySecret);