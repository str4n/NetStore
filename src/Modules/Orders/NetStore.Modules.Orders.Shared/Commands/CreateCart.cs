using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Shared.Commands;

public sealed record CreateCart(Guid UserId) : ICommand;