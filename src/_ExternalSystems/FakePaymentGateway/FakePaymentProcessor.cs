using System.Collections.Concurrent;
using FakePaymentGateway.DTO;

namespace FakePaymentGateway;

internal sealed class FakePaymentProcessor : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<FakePaymentProcessor> _logger;
    private static readonly ConcurrentQueue<SetUpPaymentWithMetadata> PaymentsToAutomaticallyPay = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private Timer _timer;

    public FakePaymentProcessor(IServiceProvider serviceProvider, ILogger<FakePaymentProcessor> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    }

    private async void DoWork(object state)
    {
        try
        {
            await _semaphore.WaitAsync();
            var tasks = new List<Task>();

            while (PaymentsToAutomaticallyPay.TryDequeue(out var payment))
            {
                tasks.Add(AutoPay(payment));
            }

            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SetUpPayment(SetUpPaymentWithMetadata payment)
    {
        PaymentsToAutomaticallyPay.Enqueue(payment);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _timer?.Dispose();
    }

    public async Task AutoPay(SetUpPaymentWithMetadata payment)
    {
        var httpClientFactory = _serviceProvider.GetService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient("NetStore.Payments");

        var result = await httpClient.PostAsJsonAsync(payment.WebhookUrl, new PaymentWebhookDto(payment.PaymentId, payment.Secret));
        
        try
        {
            result.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
    }
}