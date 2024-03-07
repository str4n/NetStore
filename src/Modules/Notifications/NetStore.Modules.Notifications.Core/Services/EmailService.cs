using System.Text;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using NetStore.Modules.Notifications.Core.DTO;
using NetStore.Modules.Notifications.Core.Facades;
using NetStore.Modules.Orders.Shared.DTO;
using NetStore.Shared.Infrastructure;

namespace NetStore.Modules.Notifications.Core.Services;

internal sealed class EmailService : IEmailService
{
    private readonly IEmailSenderFacade _emailSender;
    private readonly IUrlShortenerFacade _urlShortener;
    private readonly AppOptions _appOptions;
    
    public EmailService(IOptions<AppOptions> appOptions, IEmailSenderFacade emailSender, IUrlShortenerFacade urlShortener)
    {
        _emailSender = emailSender;
        _urlShortener = urlShortener;
        _appOptions = appOptions.Value;
    }
    
    //TODO: Move urls to higher layer
    
    public async Task SendAccountActivation(string receiverEmail, string receiverUsername, string activationToken)
    {
        var longActivationUrl = $"{_appOptions.Url}/users-module/users/activate/{activationToken}";
        var activationUrl = await _urlShortener.Shorten(longActivationUrl);
        
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

        await _emailSender.Send(emailDto);
    }

    public async Task SendPasswordRecovery(string receiverEmail, string receiverUsername, string recoveryToken)
    {
        var longRecoveryUrl = $"{_appOptions.Url}/users-module/users/recover/{recoveryToken}";
        var recoveryUrl = await _urlShortener.Shorten(longRecoveryUrl);
        
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

        await _emailSender.Send(emailDto);
    }

    public async Task SendOrderConfirmation(string receiverEmail, string receiverUsername, OrderDto order)
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

            <p>Dear {receiverUsername},</p>

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
        await _emailSender.Send(emailDto);
    }
}