namespace NetStore.Shared.Abstractions.Messaging;

public interface IMessageBroker
{
    Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    Task SendAsync<TMessage, TResult>(TMessage message) where TMessage : class, IMessage;
}