using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Shared.Commands;

public sealed record PreparePasswordRecovery(Guid UserId) : ICommand;