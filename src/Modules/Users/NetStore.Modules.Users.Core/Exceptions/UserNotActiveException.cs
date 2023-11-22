using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Exceptions;

internal sealed class UserNotActiveException : ApiException
{
    public UserNotActiveException(string username) : base($"User: {username} is not active.", ExceptionCategory.NotFound)
    {
    }
}