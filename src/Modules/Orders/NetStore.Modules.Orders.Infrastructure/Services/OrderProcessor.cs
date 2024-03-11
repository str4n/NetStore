using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetStore.Modules.Orders.Infrastructure.EF;
using NetStore.Modules.Orders.Shared.Commands;
using NetStore.Modules.Orders.Shared.Enums;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.Types.Aggregate;

namespace NetStore.Modules.Orders.Infrastructure.Services;

public sealed class OrderProcessor : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderProcessor> _logger;
    private static readonly ConcurrentQueue<Guid> OrdersToVerify = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private Timer _timer;

    public OrderProcessor(IServiceProvider serviceProvider, ILogger<OrderProcessor> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    }
    
    
    // This code looks like this to prevent race condition
    private async void DoWork(object state)
    {
        using var scope = _serviceProvider.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
        var messageBroker = scope.ServiceProvider.GetRequiredService<IMessageBroker>();
        
        try
        {
            await _semaphore.WaitAsync();

            while (OrdersToVerify.TryDequeue(out var orderId))
            {
                var order = await dbContext.Orders
                    .Include(order => order.Lines)
                    .SingleOrDefaultAsync(x => x.Id == (AggregateId)orderId);

                var orderCanceled = false;

                foreach (var line in order.Lines)
                {
                    var product = await dbContext.Products
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Id == line.ProductId);

                    if (product is null)
                    {
                        continue;
                    }

                    if (line.Quantity > product.Stock)
                    {
                        await dbContext.Orders
                            .Where(x => x.Id == (AggregateId)orderId)
                            .ExecuteUpdateAsync(x 
                                => x.SetProperty(o => o.Status, OrderStatus.Canceled));

                        orderCanceled = true;
                        await messageBroker.PublishAsync(new OrderCanceled(orderId));
                        break;
                    }
                    
                }

                if (orderCanceled is false)
                {
                    var tasks = new List<Task>();
                    
                    foreach (var line in order.Lines)
                    {
                        var product = await dbContext.Products
                            .SingleOrDefaultAsync(x => x.Id == line.ProductId);

                        if (product is null)
                        {
                            continue;
                        }

                        product.Stock -= line.Quantity;
                        
                        tasks.Add(messageBroker.PublishAsync(new DecreaseStock(product.Id, line.Quantity)));
                    }

                    await Task.WhenAll(tasks);
                    await messageBroker.PublishAsync(new OrderProcessed(orderId));
                }
                
                await dbContext.SaveChangesAsync();
            }
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
    public void ProcessOrder(Guid orderId) => OrdersToVerify.Enqueue(orderId);

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}