using MassTransit;
using NetStore.Modules.Customers.Shared.DTO;
using NetStore.Modules.Customers.Shared.ModuleRequests;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Notifications.Core.Messaging;

internal sealed class OrderPlacedConsumer : IConsumer<OrderPlaced>
{
    private readonly IMessageBroker _messageBroker;
    private readonly IEmailService _emailService;

    public OrderPlacedConsumer(IMessageBroker messageBroker, IEmailService emailService)
    {
        _messageBroker = messageBroker;
        _emailService = emailService;
    }
    
    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        var message = context.Message;

        var customerInformation =
            await _messageBroker.SendAsync<GetCustomerInformation, CustomerInformationDto>(
                new GetCustomerInformation(message.CustomerId));

        await _emailService.SendOrderConfirmation(customerInformation.Email, customerInformation.FirstName, message.Order);
    }
}