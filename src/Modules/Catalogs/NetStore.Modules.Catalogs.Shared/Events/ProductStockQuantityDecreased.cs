using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Catalogs.Shared.Events;

public sealed record ProductStockQuantityDecreased(Guid ProductId, int Quantity) : IEvent;