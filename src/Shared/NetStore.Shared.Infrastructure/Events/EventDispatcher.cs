using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetStore.Shared.Abstractions.Events;

namespace NetStore.Shared.Infrastructure.Events;

internal sealed class EventDispatcher : IEventDispatcher, IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private static readonly ConcurrentQueue<IEvent> Events = new();
    private Timer _timer;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(StartDispatcherAsync, default, TimeSpan.Zero, TimeSpan.FromSeconds(8));
    }

    private async void StartDispatcherAsync(object state)
    {
        var eventsToDispatch = new List<Task>();

        while (Events.TryDequeue(out var @event))
        {
            eventsToDispatch.Add(DispatchAsync(@event));
        }

        await Task.WhenAll(eventsToDispatch);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
    
    public Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
    {
        Events.Enqueue(@event);

        return Task.CompletedTask;
    }

    private async Task DispatchAsync<TEvent>(TEvent @event) where TEvent : class, IEvent
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = scope.ServiceProvider.GetServices(handlerType);
        var method = handlerType.GetMethod(nameof(IEventHandler<IEvent>.HandleAsync));
        
        if (method is null)
        {
            throw new InvalidOperationException($"Event handler for '{@event.GetType().Name}' is invalid.");
        }

        var tasks = handlers.Select(x => (Task)method.Invoke(x, new object[] { @event }));
        await Task.WhenAll(tasks);
    }
}