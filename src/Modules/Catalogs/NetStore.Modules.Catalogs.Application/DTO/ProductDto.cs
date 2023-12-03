namespace NetStore.Modules.Catalogs.Application.DTO;

public sealed record ProductDto(Guid Id, string Name, string Description, string Category, string Brand,
    string Model, double GrossPrice, string Fabric, string Gender, string AgeCategory, string Size, 
    string Color);