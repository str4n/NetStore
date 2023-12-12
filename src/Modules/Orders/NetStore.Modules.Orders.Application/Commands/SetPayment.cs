using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Application.Commands;

public record SetPayment(string PaymentMethod) : ICommand;