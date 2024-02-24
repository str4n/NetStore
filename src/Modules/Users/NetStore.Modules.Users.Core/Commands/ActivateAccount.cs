using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands;

public sealed record ActivateAccount(string ActivationSecret) : ICommand;