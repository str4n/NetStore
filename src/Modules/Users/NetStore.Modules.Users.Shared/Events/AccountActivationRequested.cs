using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record AccountActivationRequested(string Email, string Username, string ActivationToken) : IEvent;
