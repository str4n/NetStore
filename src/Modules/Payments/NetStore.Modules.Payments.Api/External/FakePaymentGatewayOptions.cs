namespace NetStore.Modules.Payments.Api.External;

public class FakePaymentGatewayOptions
{
    public string PaymentGatewayUrl { get; set; }
    public string WebhookUrl { get; set; }
}