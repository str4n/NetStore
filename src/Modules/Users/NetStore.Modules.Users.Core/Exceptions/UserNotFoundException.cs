using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Exceptions;

internal sealed class UserNotFoundException : ApiException
{
    public UserNotFoundException(Guid id) : base($"User with id: '{id}' was not found.", ExceptionCategory.NotFound)
    {
    }
}