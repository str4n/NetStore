using Humanizer;
using NetStore.Modules.Catalogs.Domain.Product;

namespace NetStore.Modules.Catalogs.Application.Services;

internal sealed class ProductCodeNameGenerator : IProductCodeNameGenerator
{
    public string Generate(Product product)
        => $"{product.Model.Value.Kebaberize().ToLowerInvariant()}" +
           $"-{product.Size.ToString().ToLowerInvariant()}" +
           $"-{product.Color.ToString().ToLowerInvariant()}" +
           $"-{product.AgeCategory.ToString().ToLowerInvariant()}";
}