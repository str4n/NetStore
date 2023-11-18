using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.Exceptions;

internal sealed class InvalidDiscountException : ApiException
{
    public InvalidDiscountException(string message) : base(message)
    {
    }
}