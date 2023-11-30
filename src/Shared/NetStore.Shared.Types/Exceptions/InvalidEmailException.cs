using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Shared.Types.Exceptions;

internal sealed class InvalidEmailException : ApiException
{
    public InvalidEmailException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}