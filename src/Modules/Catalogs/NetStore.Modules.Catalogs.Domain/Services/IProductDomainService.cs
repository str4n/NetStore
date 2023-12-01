using NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

namespace NetStore.Modules.Catalogs.Domain.Services;

public interface IProductDomainService
{
    void SetProductPrice(Product.Product product);
}