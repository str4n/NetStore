using MassTransit;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Notifications.Shared.Commands;

namespace NetStore.Modules.Notifications.Core.Messaging;

internal sealed class SendRecoveryEmailConsumer : IConsumer<SendRecoveryEmail>
{
    private readonly IEmailService _emailService;

    public SendRecoveryEmailConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    public async Task Consume(ConsumeContext<SendRecoveryEmail> context)
    {
        var (email, username, token) = context.Message;

        await _emailService.SendPasswordRecovery(email, username, token);
    }
}