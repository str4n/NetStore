using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.Exceptions;

internal sealed class InvalidUsernameException : ApiException
{
    public InvalidUsernameException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}