namespace NetStore.Modules.Orders.Api.External;

public class FakePaymentGatewayOptions
{
    public string PaymentGatewayUrl { get; set; }
    public string WebhookUrl { get; set; }
}