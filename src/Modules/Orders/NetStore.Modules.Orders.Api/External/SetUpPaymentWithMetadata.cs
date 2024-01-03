namespace NetStore.Modules.Orders.Api.External;

public record SetUpPaymentWithMetadata(Guid PaymentId, string PayerFullName, string Address, double Amount, 
    string Secret, string WebhookUrl);