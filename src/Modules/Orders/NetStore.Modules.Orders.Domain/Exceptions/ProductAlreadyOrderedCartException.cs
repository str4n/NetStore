using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class ProductAlreadyOrderedCartException : ApiException
{
    public ProductAlreadyOrderedCartException(Guid id) : base($"Product with id: '{id}' was already ordered.", ExceptionCategory.BadRequest)
    {
    }
}