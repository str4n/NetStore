using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Payments.Shared.Events;

public sealed record PaymentCanceled(Guid PaymentId) : IEvent;