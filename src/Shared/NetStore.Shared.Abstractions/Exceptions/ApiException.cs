namespace NetStore.Shared.Abstractions.Exceptions;

public abstract class ApiException : Exception
{
    protected ApiException(string message) : base(message)
    {
    }
}