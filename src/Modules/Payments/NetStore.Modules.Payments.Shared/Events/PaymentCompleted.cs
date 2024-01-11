using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Payments.Shared.Events;

public sealed record PaymentCompleted(Guid PaymentId) : IEvent;