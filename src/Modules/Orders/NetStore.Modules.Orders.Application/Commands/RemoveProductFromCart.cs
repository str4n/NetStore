using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Application.Commands;

public sealed record RemoveProductFromCart(Guid Id, int Quantity) : ICommand;