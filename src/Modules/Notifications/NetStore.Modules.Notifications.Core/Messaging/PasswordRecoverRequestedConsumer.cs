using MassTransit;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Users.Shared.Events;

namespace NetStore.Modules.Notifications.Core.Messaging;

internal sealed class PasswordRecoverRequestedConsumer : IConsumer<PasswordRecoverRequested>
{
    private readonly IEmailService _emailService;

    public PasswordRecoverRequestedConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    public async Task Consume(ConsumeContext<PasswordRecoverRequested> context)
    {
        var message = context.Message;

        await _emailService.SendPasswordRecover(message.Email, message.Username, message.RecoveryToken);
    }
}