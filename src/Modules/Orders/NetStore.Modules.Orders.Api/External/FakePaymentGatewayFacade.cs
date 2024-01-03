using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using NetStore.Modules.Orders.Application.External;
using NetStore.Shared.Types.DTO;

namespace NetStore.Modules.Orders.Api.External;

internal class FakePaymentGatewayFacade : IPaymentGatewayFacade
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly FakePaymentGatewayOptions _options;

    public FakePaymentGatewayFacade(IHttpClientFactory httpClientFactory, FakePaymentGatewayOptions options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options;
    }
    
    public async Task SetUpPayment(PaymentDto payment, string fullName, string address)
    {
        var paymentGatewayUrl = _options.PaymentGatewayUrl;
        var webhookUrl = _options.WebhookUrl;

        var httpClient = _httpClientFactory.CreateClient("FakePaymentGateway");

        var result = await httpClient.PostAsJsonAsync(paymentGatewayUrl, 
            new SetUpPaymentWithMetadata(payment.Id, fullName, address, payment.Amount, payment.PaymentGatewaySecret, webhookUrl));

        // result.EnsureSuccessStatusCode();
    }
}