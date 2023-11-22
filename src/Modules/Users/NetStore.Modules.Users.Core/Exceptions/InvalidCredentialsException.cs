using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Exceptions;

internal sealed class InvalidCredentialsException : ApiException
{
    public InvalidCredentialsException() : base("Invalid credentials", ExceptionCategory.ValidationError)
    {
    }
}