using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Products.Core.Exceptions;

internal sealed class ProductNotFoundException : ApiException
{
    public ProductNotFoundException(Guid id) : base($"Product with '{id}' was not found.", ExceptionCategory.NotFound)
    {
    }
}