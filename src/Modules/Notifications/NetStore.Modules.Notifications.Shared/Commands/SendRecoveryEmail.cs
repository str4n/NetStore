using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Notifications.Shared.Commands;

public sealed record SendRecoveryEmail(string Email, string Username, string RecoveryToken) : ICommand;