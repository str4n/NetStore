namespace NetStore.Modules.Notifications.Core.DTO;

public sealed record EmailDto(string ReceiverEmail, string ReceiverUsername, string EmailSubject, string EmailBody);