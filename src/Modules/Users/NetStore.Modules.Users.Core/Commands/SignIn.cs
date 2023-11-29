using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands;

internal sealed record SignIn(string Username, string Password) : ICommand;