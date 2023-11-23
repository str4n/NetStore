using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Customers.Core.Domain.Exceptions;

internal sealed class InvalidNameException : ApiException
{
    public InvalidNameException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}