using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Users.Core.Exceptions;

internal sealed class ActivationTokenNotFound : ApiException
{
    public ActivationTokenNotFound() : base("Activation token was not found.", ExceptionCategory.NotFound)
    {
    }
}