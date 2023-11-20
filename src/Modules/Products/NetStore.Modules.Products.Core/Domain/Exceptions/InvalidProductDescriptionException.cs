using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.Exceptions;

internal sealed class InvalidProductDescriptionException : ApiException
{
    public InvalidProductDescriptionException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}