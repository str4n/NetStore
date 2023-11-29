using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands;

internal sealed record SignUp(Guid Id, string Email, string Username, string Password) : ICommand;