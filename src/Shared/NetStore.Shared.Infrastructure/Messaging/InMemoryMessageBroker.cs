using NetStore.Shared.Abstractions.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Shared.Infrastructure.Messaging;

internal sealed class InMemoryMessageBroker : IMessageBroker
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly ICrossModuleQueryDispatcher _queryDispatcher;

    public InMemoryMessageBroker(IEventDispatcher eventDispatcher, ICrossModuleQueryDispatcher queryDispatcher)
    {
        _eventDispatcher = eventDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    public Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
    {
        if (typeof(TMessage).IsAssignableTo(typeof(IEvent)))
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            _eventDispatcher.PublishAsync(message as IEvent);
        }
        
        //TODO: Publishing other type of message

        return Task.CompletedTask;
    }

    public Task SendAsync<TMessage, TResult>(TMessage message) where TMessage : class, IMessage
    {
        if (typeof(TMessage).IsAssignableTo(typeof(ICrossModuleQuery<TResult>)))
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            return _queryDispatcher.SendAsync(message as ICrossModuleQuery<TResult>);
        }
        
        //TODO: Sending other type of message

        return Task.CompletedTask;
    }
}