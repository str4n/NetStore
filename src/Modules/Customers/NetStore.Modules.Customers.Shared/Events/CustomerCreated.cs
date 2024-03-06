using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Customers.Shared.Events;

public sealed record CustomerCreated(Guid UserId, string Email) : IEvent;
