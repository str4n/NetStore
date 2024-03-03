using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Exceptions;

internal sealed class UserNotFoundException : ApiException
{
    public UserNotFoundException() : base($"User was not found.", ExceptionCategory.NotFound)
    {
    }
}