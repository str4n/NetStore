using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class InvalidCartProductQuantityException : ApiException
{
    public InvalidCartProductQuantityException() : base("Product quantity must be positive number", ExceptionCategory.ValidationError)
    {
    }
}