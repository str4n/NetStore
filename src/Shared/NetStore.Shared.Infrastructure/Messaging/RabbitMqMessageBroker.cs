using MassTransit;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Shared.Infrastructure.Messaging;

internal sealed class RabbitMqMessageBroker : IMessageBroker
{
    private readonly IPublishEndpoint _publishEndpoint;

    public RabbitMqMessageBroker(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
        => _publishEndpoint.Publish(message);

    public Task SendAsync<TMessage, TResult>(TMessage message) where TMessage : class, IMessage
    {
        throw new NotImplementedException();
    }
}