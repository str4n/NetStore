using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.Exceptions;

internal sealed class InvalidPasswordException : ApiException
{
    public InvalidPasswordException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}