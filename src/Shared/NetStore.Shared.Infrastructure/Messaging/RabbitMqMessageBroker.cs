using MassTransit;
using Microsoft.Extensions.Logging;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Shared.Infrastructure.Messaging;

internal sealed class RabbitMqMessageBroker : IMessageBroker
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IModuleRequestDispatcher _moduleRequestDispatcher;
    private readonly ILogger<RabbitMqMessageBroker> _logger;

    public RabbitMqMessageBroker(IPublishEndpoint publishEndpoint, IModuleRequestDispatcher moduleRequestDispatcher,
        ILogger<RabbitMqMessageBroker> logger)
    {
        _publishEndpoint = publishEndpoint;
        _moduleRequestDispatcher = moduleRequestDispatcher;
        _logger = logger;
    }

    public Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
    {
        _logger.LogInformation("Processing message: {message}", message);
        _publishEndpoint.Publish(message);

        return Task.CompletedTask;
    }

    public async Task<TResult> SendAsync<TRequest, TResult>(TRequest request) where TRequest : class, IModuleRequest
    {
        return await _moduleRequestDispatcher.SendAsync<TResult>(request);
    }
}