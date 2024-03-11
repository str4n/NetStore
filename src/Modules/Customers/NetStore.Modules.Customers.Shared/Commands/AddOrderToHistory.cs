using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Customers.Shared.Commands;

public sealed record AddOrderToHistory(Guid OrderId) : ICommand;