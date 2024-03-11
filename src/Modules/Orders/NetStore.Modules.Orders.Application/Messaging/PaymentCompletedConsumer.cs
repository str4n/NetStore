using MassTransit;
using NetStore.Modules.Orders.Application.PaymentRegistry;
using NetStore.Modules.Orders.Domain.Services;
using NetStore.Modules.Payments.Shared.Events;

namespace NetStore.Modules.Orders.Application.Messaging;

internal sealed class PaymentCompletedConsumer : IConsumer<PaymentCompleted>
{
    private readonly IPaymentRegistry _paymentRegistry;
    private readonly IOrderDomainService _orderDomainService;

    public PaymentCompletedConsumer(IPaymentRegistry paymentRegistry, IOrderDomainService orderDomainService)
    {
        _paymentRegistry = paymentRegistry;
        _orderDomainService = orderDomainService;
    }
    
    public async Task Consume(ConsumeContext<PaymentCompleted> context)
    {
        var message = context.Message;

        var registryEntry = await _paymentRegistry.Get(message.PaymentId);

        await _orderDomainService.PayOrder(registryEntry.OrderId);
    }
}