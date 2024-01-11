using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Payments.Core.Exceptions;

internal sealed class CustomerNotFoundException : ApiException
{
    public CustomerNotFoundException(Guid id) : base($"Customer with id: '{id}' was not found.", ExceptionCategory.NotFound)
    {
    }
}