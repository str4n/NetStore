using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record UserCreated(Guid Id, string Email) : IEvent;