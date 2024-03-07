using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record PasswordRecoveryPrepared(Guid UserId, string Token) : IEvent;