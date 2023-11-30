using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Exceptions;

internal sealed class EmptyValueException : ApiException
{
    public EmptyValueException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}