using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Application.Commands;

public sealed record AddProductToCart(Guid Id, int Quantity) : ICommand;