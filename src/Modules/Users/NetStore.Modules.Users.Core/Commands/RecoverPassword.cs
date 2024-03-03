using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands;

public sealed record RecoverPassword(string Email, string NewPassword, string RecoveryToken) : ICommand;