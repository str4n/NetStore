using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Exceptions;

internal sealed class ValueLenghtException : ApiException
{
    public ValueLenghtException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}