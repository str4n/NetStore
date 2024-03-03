using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Exceptions;

internal sealed class UserMismatchException : ApiException
{
    public UserMismatchException() : base("User mismatch.", ExceptionCategory.ValidationError)
    {
    }
}