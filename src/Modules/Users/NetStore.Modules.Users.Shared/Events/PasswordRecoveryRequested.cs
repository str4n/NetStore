using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record PasswordRecoveryRequested(Guid UserId, string Email, string Username) : IEvent;