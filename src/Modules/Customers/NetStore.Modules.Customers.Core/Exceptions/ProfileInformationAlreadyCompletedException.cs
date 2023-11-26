using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Customers.Core.Exceptions;

internal sealed class ProfileInformationAlreadyCompletedException : ApiException
{
    public ProfileInformationAlreadyCompletedException() : base("Profile information is already completed.", ExceptionCategory.AlreadyExists)
    {
    }
}