using MassTransit;
using Microsoft.Extensions.Logging;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Shared.Infrastructure.Messaging;

internal sealed class RabbitMqMessageBroker : IMessageBroker
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<RabbitMqMessageBroker> _logger;

    public RabbitMqMessageBroker(IPublishEndpoint publishEndpoint, ILogger<RabbitMqMessageBroker> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
    {
        _logger.LogInformation("Processing message: {message}", message);
        _publishEndpoint.Publish(message);

        return Task.CompletedTask;
    }

    public Task SendAsync<TMessage, TResult>(TMessage message) where TMessage : class, IMessage
    {
        throw new NotImplementedException();
    }
}