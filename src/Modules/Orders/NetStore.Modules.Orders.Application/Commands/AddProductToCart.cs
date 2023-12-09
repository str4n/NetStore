using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Application.Commands;

public sealed record AddProductToCart(string ProductName, int Quantity) : ICommand;