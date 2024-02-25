namespace NetStore.Modules.Notifications.Core.Services;

public interface IEmailService
{
    Task SendAccountActivation(string receiverEmail, string receiverUsername, string activationToken);
}