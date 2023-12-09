using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Application.Exceptions;

internal sealed class NotEnoughProductsOnStockException : ApiException
{
    public NotEnoughProductsOnStockException() : base("There is not enough products on stock.", ExceptionCategory.BadRequest)
    {
    }
}