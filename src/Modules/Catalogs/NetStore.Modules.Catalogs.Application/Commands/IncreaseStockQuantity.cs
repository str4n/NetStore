using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands;

public sealed record IncreaseStockQuantity(Guid ProductId, int Quantity) : ICommand;