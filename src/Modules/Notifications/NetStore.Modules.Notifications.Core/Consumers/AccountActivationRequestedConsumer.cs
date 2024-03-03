using MassTransit;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Users.Shared.Events;

namespace NetStore.Modules.Notifications.Core.Consumers;

internal sealed class AccountActivationRequestedConsumer : IConsumer<AccountActivationRequested>
{
    private readonly IEmailService _emailService;

    public AccountActivationRequestedConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }
    

    public async Task Consume(ConsumeContext<AccountActivationRequested> context)
    {
        var message = context.Message;

        await _emailService.SendAccountActivation(message.Email, message.Username, message.ActivationToken);
    }
}