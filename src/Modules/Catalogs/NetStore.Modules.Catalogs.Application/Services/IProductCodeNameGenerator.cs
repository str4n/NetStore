using NetStore.Modules.Catalogs.Domain.Product;

namespace NetStore.Modules.Catalogs.Application.Services;

public interface IProductCodeNameGenerator
{
    string Generate(Product product);
}