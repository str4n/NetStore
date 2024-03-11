using MassTransit;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Notifications.Shared.Commands;
using NetStore.Modules.Orders.Shared.DTO;
using NetStore.Modules.Orders.Shared.ModuleRequests;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Notifications.Core.Messaging;

internal sealed class SendOrderConfirmationEmailConsumer : IConsumer<SendOrderConfirmationEmail>
{
    private readonly IEmailService _emailService;
    private readonly IMessageBroker _messageBroker;

    public SendOrderConfirmationEmailConsumer(IEmailService emailService, IMessageBroker messageBroker)
    {
        _emailService = emailService;
        _messageBroker = messageBroker;
    }
    
    public async Task Consume(ConsumeContext<SendOrderConfirmationEmail> context)
    {
        var message = context.Message;

        var orderDto = await _messageBroker.SendAsync<GetOrder, OrderDto>(new GetOrder(message.OrderId));

        await _emailService.SendOrderConfirmation(message.Email, orderDto);
    }
}