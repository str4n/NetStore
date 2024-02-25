using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace NetStore.Modules.Notifications.Core.Services;

internal sealed class EmailService : IEmailService
{
    private readonly EmailSenderOptions _options;
    
    public EmailService(IOptions<EmailSenderOptions> options)
    {
        _options = options.Value;
    }
    
    //TODO: Make this method more generic
    
    public async Task SendAccountActivation(string receiverEmail, string receiverUsername, string activationToken)
    {
        using var email = new MimeMessage();
        var sender = new MailboxAddress(_options.SenderName, _options.SenderEmail);
        email.From.Add(sender);

        var receiver = new MailboxAddress(receiverUsername, receiverEmail);
        email.To.Add(receiver);

        email.Subject = "NetStore account activation";

        //TODO: Move the url to configuration
        
        var emailActivationUrl = $"https://localhost:7240/users/activate/{activationToken}";
                
        var emailBodyBuilder = new BodyBuilder
        {
            TextBody = $"Here is your account activation link: {emailActivationUrl}"
        };

        email.Body = emailBodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();
            
        await smtpClient.ConnectAsync(_options.Server, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_options.UserName, _options.Password);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }
}