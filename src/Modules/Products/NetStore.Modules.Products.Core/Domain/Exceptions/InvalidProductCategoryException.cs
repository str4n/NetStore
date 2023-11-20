using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.Exceptions;

internal sealed class InvalidProductCategoryException : ApiException
{
    public InvalidProductCategoryException(string message) : base(message, ExceptionCategory.ValidationError)
    {
    }
}