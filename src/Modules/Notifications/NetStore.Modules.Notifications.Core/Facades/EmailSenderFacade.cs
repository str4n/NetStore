using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using NetStore.Modules.Notifications.Core.DTO;

namespace NetStore.Modules.Notifications.Core.Facades;

internal sealed class EmailSenderFacade : IEmailSenderFacade
{
    private readonly EmailSenderOptions _emailSenderOptions;
    
    public EmailSenderFacade(IOptions<EmailSenderOptions> emailSenderOptions)
    {
        _emailSenderOptions = emailSenderOptions.Value;
    }
    
    public async Task Send(EmailDto dto)
    {
        using var email = new MimeMessage();
        var sender = new MailboxAddress(_emailSenderOptions.SenderName, _emailSenderOptions.SenderEmail);
        email.From.Add(sender);

        var receiver = new MailboxAddress(dto.ReceiverUsername, dto.ReceiverEmail);
        email.To.Add(receiver);

        email.Subject = dto.EmailSubject;
        
        var emailBodyBuilder = new BodyBuilder
        {
            HtmlBody = dto.EmailBody,
        };
        
        email.Body = emailBodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();
            
        await smtpClient.ConnectAsync(_emailSenderOptions.Server, _emailSenderOptions.Port, MailKit.Security.SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_emailSenderOptions.UserName, _emailSenderOptions.Password);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }
}