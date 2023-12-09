using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Application.Exceptions;

internal sealed class CartErrorException : ApiException
{
    public CartErrorException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}