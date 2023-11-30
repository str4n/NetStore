using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Shared.Types.Exceptions;

internal sealed class InvalidRoleException : ApiException
{
    public InvalidRoleException() : base("Role name is invalid. Valid role names are: [ user, admin ]", ExceptionCategory.ValidationError)
    {
    }
}