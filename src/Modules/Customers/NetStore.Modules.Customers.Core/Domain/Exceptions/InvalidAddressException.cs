using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Customers.Core.Domain.Exceptions;

internal sealed class InvalidAddressException : ApiException
{
    public InvalidAddressException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}