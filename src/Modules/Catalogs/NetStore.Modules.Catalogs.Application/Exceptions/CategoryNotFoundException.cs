using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class CategoryNotFoundException : ApiException
{
    public CategoryNotFoundException() : base("Category was not found.", ExceptionCategory.NotFound)
    {
    }
}