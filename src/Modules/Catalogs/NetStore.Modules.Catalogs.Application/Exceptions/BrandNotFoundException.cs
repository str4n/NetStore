using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class BrandNotFoundException : ApiException
{
    public BrandNotFoundException() : base("Brand was not found.", ExceptionCategory.NotFound)
    {
    }
}