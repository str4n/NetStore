namespace NetStore.Modules.Products.Core.DTO;

public sealed record ProductDto(Guid Id, string Name, string Description, IEnumerable<string> Categories, double Price, int Discount);