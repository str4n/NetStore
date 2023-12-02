using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

namespace NetStore.Modules.Catalogs.Domain.Services;

internal sealed class ProductDomainService : IProductDomainService
{
    public void SetProductPrice(Product.Product product, ProductPrice netPrice)
    {
        var ageCategory = product.AgeCategory;
        
        var grossPrice = ageCategory switch
        {
            AgeCategory.Child or AgeCategory.Teenager => netPrice.Value + netPrice * 0.05,
            AgeCategory.Adult => netPrice.Value + netPrice * 0.23,
            _ => throw new ArgumentOutOfRangeException(nameof(ageCategory), ageCategory, null)
        };
        
        product.ChangePrice(netPrice, grossPrice);
    }
}