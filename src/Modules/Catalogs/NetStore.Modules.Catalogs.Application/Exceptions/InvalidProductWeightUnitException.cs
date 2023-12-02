using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Catalogs.Application.Exceptions;

internal sealed class InvalidProductWeightUnitException : ApiException
{
    public InvalidProductWeightUnitException() : base("Invalid product weight unit. Correct units: [ Kilogram, Gram ]", ExceptionCategory.ValidationError)
    {
    }
}