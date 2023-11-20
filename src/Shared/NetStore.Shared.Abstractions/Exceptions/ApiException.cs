namespace NetStore.Shared.Abstractions.Exceptions;

public abstract class ApiException : Exception
{
    public ExceptionCategory ExceptionCategory { get; }
    protected ApiException(string message, ExceptionCategory exceptionCategory) : base(message)
    {
        ExceptionCategory = exceptionCategory;
    }
}

public enum ExceptionCategory
{
    ValidationError,
    NotFound,
    AlreadyExists
}