using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class ProductAlreadyInDifferentCartException : ApiException
{
    public ProductAlreadyInDifferentCartException(Guid id) : base($"Product with id: '{id}' is already in different cart.", ExceptionCategory.BadRequest)
    {
    }
}