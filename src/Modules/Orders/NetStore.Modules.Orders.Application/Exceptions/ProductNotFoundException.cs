using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Application.Exceptions;

internal sealed class ProductNotFoundException : ApiException
{
    public ProductNotFoundException(string name) : base($"Product with name: '{name}' was not found.", ExceptionCategory.NotFound)
    {
    }
}