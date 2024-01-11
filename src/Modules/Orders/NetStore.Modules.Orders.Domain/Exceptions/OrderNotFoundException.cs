using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Domain.Exceptions;

internal sealed class OrderNotFoundException : ApiException
{
    public OrderNotFoundException(Guid id) : base($"Order with id: '{id}' was not found.", ExceptionCategory.NotFound)
    {
    }
}