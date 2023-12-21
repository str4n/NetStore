using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class ProductNotFoundException : ApiException
{
    public ProductNotFoundException() : base("Product was not found.", ExceptionCategory.NotFound)
    {
    }
}