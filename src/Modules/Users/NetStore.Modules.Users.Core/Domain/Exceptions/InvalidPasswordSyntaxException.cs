using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.Exceptions;

internal sealed class InvalidPasswordSyntaxException : ApiException
{
    public InvalidPasswordSyntaxException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}