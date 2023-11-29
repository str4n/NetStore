using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands;

internal sealed record DeleteUser(Guid Id) : ICommand;