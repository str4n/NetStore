using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Exceptions;

internal sealed class StockCannotBeNegativeValueException : ApiException
{
    public StockCannotBeNegativeValueException() : base("The stock cannot be less than 0", ExceptionCategory.ValidationError)
    {
    }
}