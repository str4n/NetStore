using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class InvalidProductSizeException : ApiException
{
    public InvalidProductSizeException() : base("Invalid product size. Correct sizes : [ XS, S, M, L, XL, XXL ]", ExceptionCategory.ValidationError)
    {
    }
}