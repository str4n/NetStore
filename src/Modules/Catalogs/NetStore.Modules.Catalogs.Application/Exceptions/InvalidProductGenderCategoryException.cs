using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class InvalidProductGenderCategoryException : ApiException
{
    public InvalidProductGenderCategoryException() : base("Invalid product gender category. Correct gender categories: [ male, female, unisex]", ExceptionCategory.ValidationError)
    {
    }
}