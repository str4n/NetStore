using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record PasswordRecoverRequested(string Email, string Username, string RecoveryToken) : IEvent;