using MassTransit;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Notifications.Shared.Commands;

namespace NetStore.Modules.Notifications.Core.Messaging;

internal sealed class SendActivationEmailConsumer : IConsumer<SendActivationEmail>
{
    private readonly IEmailService _emailService;

    public SendActivationEmailConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    public async Task Consume(ConsumeContext<SendActivationEmail> context)
    {
        var (email, username, token) = context.Message;
        
        await _emailService.SendAccountActivation(email, username, token);
    }
}