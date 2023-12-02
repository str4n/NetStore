using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands;

public sealed record CreateProducts(long MockupId, int Count, string AgeCategory, string Size, string Color, 
    double Price, double Weight, string WeightUnit) : ICommand;