using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class InvalidProductColorException : ApiException
{
    public InvalidProductColorException() : base("Invalid product color.", ExceptionCategory.ValidationError)
    {
    }
}