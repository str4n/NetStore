using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Users.Shared.Events;

public sealed record UserAccountActivationRequested(string Email, string Username, string ActivationToken) : IEvent;
