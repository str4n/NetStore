using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.Exceptions;

internal sealed class InvalidProductPriceException : ApiException
{
    public InvalidProductPriceException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}