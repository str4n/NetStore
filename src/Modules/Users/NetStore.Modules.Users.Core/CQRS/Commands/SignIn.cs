using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.CQRS.Commands;

internal sealed record SignIn(string Username, string Password) : ICommand;