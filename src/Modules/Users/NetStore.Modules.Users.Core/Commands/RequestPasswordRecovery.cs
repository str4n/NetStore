using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands;

public sealed record RequestPasswordRecovery(string Email) : ICommand;