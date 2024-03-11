using MassTransit;
using NetStore.Modules.Orders.Application.PaymentRegistry;
using NetStore.Modules.Payments.Shared.Commands;

namespace NetStore.Modules.Orders.Application.Messaging;

internal sealed class RequestPaymentConsumer : IConsumer<RequestPayment>
{
    private readonly IPaymentRegistry _paymentRegistry;

    public RequestPaymentConsumer(IPaymentRegistry paymentRegistry)
    {
        _paymentRegistry = paymentRegistry;
    }
    
    public async Task Consume(ConsumeContext<RequestPayment> context)
    {
        var paymentId = context.Message.PaymentId;
        var orderId = context.Message.OrderId;
        
        var entry = new PaymentRegistryEntry(paymentId, orderId);

        await _paymentRegistry.Set(entry);
    }
}