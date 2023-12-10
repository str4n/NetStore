using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Application.Commands;

public sealed record RemoveProductFromCart(string CodeName, int Quantity) : ICommand;