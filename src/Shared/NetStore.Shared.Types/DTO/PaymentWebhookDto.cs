namespace NetStore.Shared.Types.DTO;

public sealed record PaymentWebhookDto(Guid PaymentId, string PaymentGatewaySecret);