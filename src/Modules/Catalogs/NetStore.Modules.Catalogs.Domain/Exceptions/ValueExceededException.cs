using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Exceptions;

internal sealed class ValueExceededException : ApiException
{
    public ValueExceededException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}