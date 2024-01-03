using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Orders.Application.Exceptions;

internal sealed class CustomerInfoNotCompletedException : ApiException
{
    public CustomerInfoNotCompletedException() : base("Complete your profile information.", ExceptionCategory.BadRequest)
    {
    }
}