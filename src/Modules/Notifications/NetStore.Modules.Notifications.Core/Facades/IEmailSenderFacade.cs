using NetStore.Modules.Notifications.Core.DTO;

namespace NetStore.Modules.Notifications.Core.Facades;

public interface IEmailSenderFacade
{
    Task Send(EmailDto dto);
}