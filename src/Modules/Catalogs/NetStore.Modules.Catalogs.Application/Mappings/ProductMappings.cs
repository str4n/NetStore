using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Domain.Product;

namespace NetStore.Modules.Catalogs.Application.Mappings;

internal static class ProductMappings
{
    public static ProductDto AsDto(this Product product)
        => new(product.Id, product.Name, product.Description, product.Category.Name, product.Brand.Name,
            product.Model.Value, product.GrossPrice, product.Fabric.Value, product.Gender.ToString(),
            product.AgeCategory.ToString(), product.Size.ToString(), product.Color.ToString(), product.Stock);
}