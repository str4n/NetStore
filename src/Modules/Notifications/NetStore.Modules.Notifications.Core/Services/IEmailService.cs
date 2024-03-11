using NetStore.Modules.Orders.Shared.DTO;

namespace NetStore.Modules.Notifications.Core.Services;

public interface IEmailService
{
    Task SendAccountActivation(string receiverEmail, string receiverUsername, string activationToken);
    Task SendPasswordRecovery(string receiverEmail, string receiverUsername, string recoveryToken);
    Task SendOrderConfirmation(string receiverEmail, OrderDto order);
}