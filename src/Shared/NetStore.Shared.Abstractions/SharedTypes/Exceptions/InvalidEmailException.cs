using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Shared.Abstractions.SharedTypes.Exceptions;

internal sealed class InvalidEmailException : ApiException
{
    public InvalidEmailException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}