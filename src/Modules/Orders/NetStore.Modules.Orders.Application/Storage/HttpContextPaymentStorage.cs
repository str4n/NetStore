using Microsoft.AspNetCore.Http;

namespace NetStore.Modules.Orders.Application.Storage;

internal sealed class HttpContextPaymentStorage : IPaymentStorage
{
    private const string PaymentKey = "payment";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextPaymentStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(Payment payment) => _httpContextAccessor.HttpContext?.Items.TryAdd(PaymentKey, payment);

    public Payment Get()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }
        
        if (_httpContextAccessor.HttpContext.Items.TryGetValue(PaymentKey, out var payment))
        {
            return payment as Payment;
        }

        return null;
    }
}