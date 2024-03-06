using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Shared.Abstractions.Messaging;

public interface IMessageBroker
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    Task<TResult> SendAsync<TRequest, TResult>(TRequest request) where TRequest : class, IModuleRequest;
}