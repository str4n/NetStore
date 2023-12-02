using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands;

public sealed record CreateProductMockup(string Name, string Description, string CategoryCode, string Brand, string Model, 
    string Fabric, string Gender) : ICommand;