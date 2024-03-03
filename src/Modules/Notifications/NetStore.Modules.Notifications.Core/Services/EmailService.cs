using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using NetStore.Modules.Notifications.Core.DTO;
using NetStore.Shared.Infrastructure;

namespace NetStore.Modules.Notifications.Core.Services;

internal sealed class EmailService : IEmailService
{
    private readonly IUrlShortener _urlShortener;
    private readonly EmailSenderOptions _emailSenderOptions;
    private readonly AppOptions _appOptions;
    
    public EmailService(IOptions<EmailSenderOptions> emailSenderOptions, IOptions<AppOptions> appOptions, IUrlShortener urlShortener)
    {
        _urlShortener = urlShortener;
        _emailSenderOptions = emailSenderOptions.Value;
        _appOptions = appOptions.Value;
    }
    
    //TODO: Email templates
    //TODO: Move urls to higher layer
    
    public async Task SendAccountActivation(string receiverEmail, string receiverUsername, string activationToken)
    {
        var longActivationUrl = $"{_appOptions.Url}/users-module/users/activate/{activationToken}";
        string activationUrl;

        if (_appOptions.UseUrlShortener)
        {
            activationUrl = await _urlShortener.ShortenUrl(longActivationUrl);
        }
        else
        {
            activationUrl = longActivationUrl;
        }
        
        
        var emailSubject = "NetStore account activation";
        var emailBody = $@"<h1>Welcome</h1><br><p>We're excited to have you get started. First, you need to confirm your account. <a href=""{activationUrl}"">Activate account</a> </p><br><br><p>NetStore Team</p>";

        var emailDto = new EmailDto(receiverEmail, receiverUsername, emailSubject, emailBody);

        await SendEmail(emailDto);
    }

    public async Task SendPasswordRecover(string receiverEmail, string receiverUsername, string recoveryToken)
    {
        var longActivationUrl = $"{_appOptions.Url}/users-module/users/recover/{recoveryToken}";
        string activationUrl;

        if (_appOptions.UseUrlShortener)
        {
            activationUrl = await _urlShortener.ShortenUrl(longActivationUrl);
        }
        else
        {
            activationUrl = longActivationUrl;
        }
        
        
        var emailSubject = "NetStore password recovery";
        var emailBody = $@"<h1>Welcome</h1><br><p>Click a link to recover password. <a href=""{activationUrl}"">Recover password</a> </p><br><br><p>NetStore Team</p>";

        var emailDto = new EmailDto(receiverEmail, receiverUsername, emailSubject, emailBody);

        await SendEmail(emailDto);
    }

    private async Task SendEmail(EmailDto emailData)
    {
        using var email = new MimeMessage();
        var sender = new MailboxAddress(_emailSenderOptions.SenderName, _emailSenderOptions.SenderEmail);
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
            
        await smtpClient.ConnectAsync(_emailSenderOptions.Server, _emailSenderOptions.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_emailSenderOptions.UserName, _emailSenderOptions.Password);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }
}