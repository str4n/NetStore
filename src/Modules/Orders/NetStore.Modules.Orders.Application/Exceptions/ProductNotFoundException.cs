using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Application.Exceptions;

internal sealed class ProductNotFoundException : ApiException
{
    public ProductNotFoundException() : base($"Product was not found.", ExceptionCategory.NotFound)
    {
    }
}