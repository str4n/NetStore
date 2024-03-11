namespace NetStore.Modules.Payments.Api.External;

public sealed record SetUpPaymentWithMetadata(Guid PaymentId, string PayerFullName, string Address, double Amount, 
    string Secret, string WebhookUrl);