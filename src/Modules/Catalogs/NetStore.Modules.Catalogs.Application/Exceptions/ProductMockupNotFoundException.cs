using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class ProductMockupNotFoundException : ApiException
{
    public ProductMockupNotFoundException() : base("Product mockup was not found.", ExceptionCategory.NotFound)
    {
    }
}