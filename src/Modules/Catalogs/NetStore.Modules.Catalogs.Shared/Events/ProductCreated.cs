using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Catalogs.Shared.Events;

public sealed record ProductCreated(Guid Id, string Name, string SKU, string CodeName, string Size, string Color, double Price) : IEvent;
