using MassTransit;
using NetStore.Modules.Payments.Core.Services;
using NetStore.Modules.Payments.Shared.Commands;
using NetStore.Modules.Payments.Shared.DTO;

namespace NetStore.Modules.Payments.Core.Messaging;

internal sealed class RequestPaymentConsumer : IConsumer<RequestPayment>
{
    private readonly IPaymentService _paymentService;

    public RequestPaymentConsumer(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    public async Task Consume(ConsumeContext<RequestPayment> context)
    {
        var message = context.Message;

        var dto = new SetupPaymentDto(message.PaymentId, message.CustomerId, message.Amount);
        
        await _paymentService.SetupPayment(dto);
    }
}