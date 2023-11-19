using NetStore.Modules.Products.Core.Domain.Entities;
using NetStore.Modules.Products.Core.Domain.ValueObjects;
using NetStore.Modules.Products.Core.DTO;

namespace NetStore.Modules.Products.Core.Mappings;

internal static class ProductMappings
{
    public static Product ToEntity(this ProductDto dto)
        // ReSharper disable once SuspiciousTypeConversion.Global
        => Product.Create(dto.Id, dto.Name, dto.Description, dto.Categories as IEnumerable<Category>,
            dto.Price, dto.Discount);

    public static ProductDto AsDto(this Product entity)
        // ReSharper disable once SuspiciousTypeConversion.Global
        => new ProductDto(entity.Id, entity.Name, entity.Description, entity.Categories as IEnumerable<string>, entity.Price,entity.Discount);
}