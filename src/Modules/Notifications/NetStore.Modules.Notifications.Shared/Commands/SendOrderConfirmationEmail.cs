using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Notifications.Shared.Commands;

public sealed record SendOrderConfirmationEmail(string Email, Guid OrderId) : ICommand;