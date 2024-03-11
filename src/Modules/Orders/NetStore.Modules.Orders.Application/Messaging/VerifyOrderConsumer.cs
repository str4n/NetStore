using MassTransit;
using NetStore.Modules.Orders.Infrastructure.Services;
using NetStore.Modules.Orders.Shared.Commands;

namespace NetStore.Modules.Orders.Application.Messaging;

internal sealed class VerifyOrderConsumer : IConsumer<ProcessOrder>
{
    private readonly OrderProcessor _orderProcessor;

    public VerifyOrderConsumer(OrderProcessor orderProcessor)
    {
        _orderProcessor = orderProcessor;
    }
    
    public Task Consume(ConsumeContext<ProcessOrder> context)
    {
        _orderProcessor.ProcessOrder(context.Message.OrderId);

        return Task.CompletedTask;
    }
}