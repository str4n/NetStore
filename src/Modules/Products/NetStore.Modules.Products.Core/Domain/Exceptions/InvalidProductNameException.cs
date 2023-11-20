using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.Exceptions;

internal sealed class InvalidProductNameException : ApiException
{
    public InvalidProductNameException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}