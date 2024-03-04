using MassTransit;
using NetStore.Modules.Customers.Shared.DTO;
using NetStore.Modules.Customers.Shared.ModuleRequests;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Modules.Notifications.Core.Consumers;

internal sealed class OrderPlacedConsumer : IConsumer<OrderPlaced>
{
    private readonly IModuleRequestDispatcher _moduleRequestDispatcher;
    private readonly IEmailService _emailService;

    public OrderPlacedConsumer(IModuleRequestDispatcher moduleRequestDispatcher, IEmailService emailService)
    {
        _moduleRequestDispatcher = moduleRequestDispatcher;
        _emailService = emailService;
    }
    
    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        var message = context.Message;

        var customerInformation =
            await _moduleRequestDispatcher.SendAsync<CustomerInformationDto>(
                new GetCustomerInformation(message.CustomerId));

        await _emailService.SendOrderConfirmation(customerInformation.Email, customerInformation.FirstName, message.Order);
    }
}