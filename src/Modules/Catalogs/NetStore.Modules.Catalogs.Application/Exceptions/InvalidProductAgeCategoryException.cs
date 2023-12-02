using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class InvalidProductAgeCategoryException : ApiException
{
    public InvalidProductAgeCategoryException() : base("Invalid product age category. Correct age categories: [ Child, Teenager, Adult ]", ExceptionCategory.ValidationError)
    {
    }
}