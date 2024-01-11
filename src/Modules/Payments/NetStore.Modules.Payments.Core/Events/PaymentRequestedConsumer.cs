using MassTransit;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Modules.Payments.Core.Services;
using NetStore.Modules.Payments.Shared.DTO;

namespace NetStore.Modules.Payments.Core.Events;

internal sealed class PaymentRequestedConsumer : IConsumer<PaymentRequested>
{
    private readonly IPaymentService _paymentService;

    public PaymentRequestedConsumer(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    public async Task Consume(ConsumeContext<PaymentRequested> context)
    {
        var message = context.Message;

        var dto = new SetupPaymentDto(message.PaymentId, message.CustomerId ,message.Amount, message.DueDate);
        
        await _paymentService.SetupPayment(dto);
    }
}