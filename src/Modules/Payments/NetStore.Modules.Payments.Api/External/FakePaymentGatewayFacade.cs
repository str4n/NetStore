using System.Net.Http.Json;
using NetStore.Modules.Payments.Core.External;
using NetStore.Modules.Payments.Shared.DTO;

namespace NetStore.Modules.Payments.Api.External;

internal class FakePaymentGatewayFacade : IPaymentGatewayFacade
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly FakePaymentGatewayOptions _options;

    public FakePaymentGatewayFacade(IHttpClientFactory httpClientFactory, FakePaymentGatewayOptions options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
    }
    
    public async Task SetUpPayment(SetupPaymentDto payment, string fullName, string address, string paymentGatewaySecret)
    {
        var paymentGatewayUrl = _options.PaymentGatewayUrl;
        var webhookUrl = _options.WebhookUrl;
        
        var httpClient = _httpClientFactory.CreateClient("FakePaymentGateway");
        
        var result = await httpClient.PostAsJsonAsync(paymentGatewayUrl, 
            new SetUpPaymentWithMetadata(payment.PaymentId, fullName, address, payment.Amount, payment.DueDate, paymentGatewaySecret, webhookUrl));

        result.EnsureSuccessStatusCode();
    }
}