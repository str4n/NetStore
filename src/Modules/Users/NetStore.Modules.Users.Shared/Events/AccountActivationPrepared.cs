using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record AccountActivationPrepared(Guid UserId, string Token) : IEvent;