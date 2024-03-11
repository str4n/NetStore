using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Payments.Shared.Commands;

public sealed record RequestPayment(Guid OrderId, Guid CustomerId, Guid PaymentId, double Amount) : ICommand;