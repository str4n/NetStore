using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Domain.Category;

namespace NetStore.Modules.Catalogs.Application.Mappings;

internal static class CategoryMappings
{
    public static CategoryDto AsDto(this Category category)
        => new CategoryDto(category.Id, category.Name, category.Description, category.Code);
}