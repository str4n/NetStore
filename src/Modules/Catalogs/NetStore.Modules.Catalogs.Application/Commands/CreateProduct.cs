using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands;

public sealed record CreateProduct(Guid Id, string Name, string Description, 
    long CategoryId, long BrandId, string Model, string Fabric, string Gender, string AgeCategory, 
    string Size, string Color, double NetPrice) : ICommand;