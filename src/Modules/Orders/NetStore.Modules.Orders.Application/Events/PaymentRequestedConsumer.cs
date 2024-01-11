using MassTransit;
using NetStore.Modules.Orders.Application.PaymentRegistry;
using NetStore.Modules.Orders.Shared.Events;

namespace NetStore.Modules.Orders.Application.Events;

internal sealed class PaymentRequestedConsumer : IConsumer<PaymentRequested>
{
    private readonly IPaymentRegistry _paymentRegistry;

    public PaymentRequestedConsumer(IPaymentRegistry paymentRegistry)
    {
        _paymentRegistry = paymentRegistry;
    }
    
    public async Task Consume(ConsumeContext<PaymentRequested> context)
    {
        var message = context.Message;

        await _paymentRegistry.Set(new PaymentRegistryEntry(message.PaymentId, message.OrderId));
    }
}