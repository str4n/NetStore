using NetStore.Modules.Orders.Shared.DTO;

namespace NetStore.Modules.Notifications.Core.Services;

public interface IEmailService
{
    Task SendAccountActivation(string receiverEmail, string receiverUsername, string activationToken);
    Task SendPasswordRecover(string receiverEmail, string receiverUsername, string recoveryToken);
    Task SendOrderConfirmation(string receiverEmail, string receiverName, OrderDto order);
}