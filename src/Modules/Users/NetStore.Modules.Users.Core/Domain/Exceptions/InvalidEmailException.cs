using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.Exceptions;

internal sealed class InvalidEmailException : ApiException
{
    public InvalidEmailException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}