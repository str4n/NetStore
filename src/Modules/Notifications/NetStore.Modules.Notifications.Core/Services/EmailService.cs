using System.Text;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using NetStore.Modules.Notifications.Core.DTO;
using NetStore.Modules.Orders.Shared.DTO;
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
        var activationUrl = await ShortenUrl(longActivationUrl);
        
        
        var emailSubject = "NetStore account activation";
        var emailBody = $@"<h2 style=""color: #333333;"">Account Activation</h2>
        <p>Dear {receiverUsername},</p>

        <p>Welcome to our community! We're thrilled to have you onboard.</p>

        <p>To activate your account, please click on the following link: <a href=""{activationUrl}"" style=""color: #007bff; text-decoration: none;"">Activation Link</a></p>

        <p>If you have any questions or need assistance, feel free to reach out to our support team at <a href=""mailto:[Support Email]"" style=""color: #007bff; text-decoration: none;"">[Support Email]</a>. We're here to help.</p>

        <p>Thank you for choosing us!</p>

        <p>Best regards,<br>
        NetStore Team";

        var emailDto = new EmailDto(receiverEmail, receiverUsername, emailSubject, emailBody);

        await SendEmail(emailDto);
    }

    public async Task SendPasswordRecover(string receiverEmail, string receiverUsername, string recoveryToken)
    {
        var longRecoveryUrl = $"{_appOptions.Url}/users-module/users/recover/{recoveryToken}";
        var recoveryUrl = await ShortenUrl(longRecoveryUrl);
        
        var emailSubject = "NetStore password recovery";
        var emailBody = $@"<h2 style=""color: #333333;"">Password Recovery Assistance</h2>

        <p>Dear {receiverUsername},</p>

        <p>It appears that you've requested assistance with recovering your password. Not to worry, we've got you covered!</p>

        <p>To reset your password, please click on the following link: <a href=""{recoveryUrl}"" style=""color: #007bff; text-decoration: none;"">Password Reset Link</a></p>

        <p>If you didn't request this change, you can safely ignore this email. Your account security is important to us, so we advise you to not share this link with anyone.</p>

        <p>If you encounter any issues or have further questions, please don't hesitate to contact our support team at <a href=""mailto:[Support Email]"" style=""color: #007bff; text-decoration: none;"">[Support Email]</a>. We're here to help.</p>

        <p>Best regards,<br>
        NetStore Team";

        var emailDto = new EmailDto(receiverEmail, receiverUsername, emailSubject, emailBody);

        await SendEmail(emailDto);
    }

    public async Task SendOrderConfirmation(string receiverEmail, string receiverName, OrderDto order)
    {
        var emailSubject = "NetStore order confirmation";
        
        var orderLinesHtml = new StringBuilder();

        foreach (var orderLine in order.Lines)
        {
            orderLinesHtml.Append($@"
                <tr>
                    <td style=""border: 1px solid #dddddd; text-align: left; padding: 8px;"">{orderLine.OrderLineNumber}</td>
                    <td style=""border: 1px solid #dddddd; text-align: left; padding: 8px;"">{orderLine.Name}</td>
                    <td style=""border: 1px solid #dddddd; text-align: left; padding: 8px;"">{orderLine.Quantity}</td>
                    <td style=""border: 1px solid #dddddd; text-align: left; padding: 8px;"">{orderLine.UnitPrice}</td>
                </tr>");
        }

        var emailBody = $@"<h2 style=""color: #333333;"">Order Confirmation</h2>

            <p>Dear {order.ReceiverName},</p>

            <p>We're excited to confirm that your order has been successfully placed!</p>

            <p>Order Details:</p>

            <table style=""width: 100%; border-collapse: collapse;"">
                <tr>
                    <th style=""border: 1px solid #dddddd; text-align: left; padding: 8px;"">Order Line</th>
                    <th style=""border: 1px solid #dddddd; text-align: left; padding: 8px;"">Product Name</th>
                    <th style=""border: 1px solid #dddddd; text-align: left; padding: 8px;"">Quantity</th>
                    <th style=""border: 1px solid #dddddd; text-align: left; padding: 8px;"">Unit Price</th>
                </tr>
                {orderLinesHtml}
            </table>

            <p>Your order will be processed shortly. You will receive a notification once it has been shipped.</p>

            <p>If you have any questions or concerns regarding your order, feel free to contact our customer support at <a href=""mailto:[Support Email]"" style=""color: #007bff; text-decoration: none;"">[Support Email]</a>.</p>

            <p>Thank you for shopping with us!</p>

            <p>Best regards,<br>
            NetStore Team";


        var emailDto = new EmailDto(receiverEmail, order.ReceiverName, emailSubject, emailBody);
        await SendEmail(emailDto);
    }

    private async Task<string> ShortenUrl(string url)
    {
        string result;
        
        if (_appOptions.UseUrlShortener)
        {
            result = await _urlShortener.ShortenUrl(url);
        }
        else
        {
            result = url;
        }

        return result;
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