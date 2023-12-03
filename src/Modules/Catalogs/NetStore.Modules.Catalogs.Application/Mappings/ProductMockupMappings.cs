using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Domain.Product.Mockup;

namespace NetStore.Modules.Catalogs.Application.Mappings;

internal static class ProductMockupMappings
{
    public static ProductMockupDto AsDto(this ProductMockup mockup)
        => new ProductMockupDto(mockup.Id, mockup.Name, mockup.Description, mockup.Model, mockup.Fabric,
            mockup.Gender.ToString());
}