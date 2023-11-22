using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.CQRS.Commands;

internal sealed record SignUp(Guid Id, string Email, string Username, string Password) : ICommand;