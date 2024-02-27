using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using NetStore.Modules.Notifications.Core.DTO;
using NetStore.Shared.Infrastructure;

namespace NetStore.Modules.Notifications.Core.Services;

internal sealed class EmailService : IEmailService
{
    private readonly EmailSenderOptions _options;
    private readonly string _baseUrl;
    
    public EmailService(IOptions<EmailSenderOptions> emailSenderOptions, IOptions<AppOptions> appOptions)
    {
        _options = emailSenderOptions.Value;
        _baseUrl = appOptions.Value.Url;
    }
    
    //TODO: Email templates
    
    public async Task SendAccountActivation(string receiverEmail, string receiverUsername, string activationToken)
    {
        var emailSubject = "NetStore account activation";
        var emailBody = $@"<h1>Welcome</h1><br><p>We're excited to have you get started. First, you need to confirm your account. <a href=""{_baseUrl}/users-module/users/activate/{activationToken}"">Activate account</a> </p><br><br><p>NetStore Team</p>";

        var emailData = new EmailDto(receiverEmail, receiverUsername, emailSubject, emailBody);

        await SendEmail(emailData);
    }

    private async Task SendEmail(EmailDto emailData)
    {
        using var email = new MimeMessage();
        var sender = new MailboxAddress(_options.SenderName, _options.SenderEmail);
        email.From.Add(sender);

        var receiver = new MailboxAddress(emailData.ReceiverUsername, emailData.ReceiverEmail);
        email.To.Add(receiver);

        email.Subject = emailData.EmailSubject;
        
        var emailBodyBuilder = new BodyBuilder
        {
            HtmlBody = emailData.EmailBody,
        };
        
        email.Body = emailBodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();
            
        await smtpClient.ConnectAsync(_options.Server, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_options.UserName, _options.Password);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }
}