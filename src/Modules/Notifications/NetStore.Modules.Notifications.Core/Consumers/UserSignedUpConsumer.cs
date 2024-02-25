using MassTransit;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Users.Shared.Events;

namespace NetStore.Modules.Notifications.Core.Consumers;

internal sealed class UserSignedUpConsumer : IConsumer<UserAccountActivationRequested>
{
    private readonly IEmailService _emailService;

    public UserSignedUpConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }
    

    public async Task Consume(ConsumeContext<UserAccountActivationRequested> context)
    {
        var message = context.Message;

        await _emailService.SendAccountActivation(message.Email, message.Username, message.ActivationToken);
    }
}