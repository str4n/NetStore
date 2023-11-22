using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Exceptions;

internal sealed class UserAlreadyExistsException : ApiException
{
    public UserAlreadyExistsException(string message) : base(message, ExceptionCategory.AlreadyExists)
    {
    }
}