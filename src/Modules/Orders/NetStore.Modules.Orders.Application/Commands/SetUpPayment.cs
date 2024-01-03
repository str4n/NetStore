using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Application.Commands;

public record SetUpPayment(string PaymentMethod) : ICommand;