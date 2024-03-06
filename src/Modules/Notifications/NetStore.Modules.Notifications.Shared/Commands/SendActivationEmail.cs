using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Notifications.Shared.Commands;

public sealed record SendActivationEmail(string Email, string Username, string ActivationToken) : ICommand;