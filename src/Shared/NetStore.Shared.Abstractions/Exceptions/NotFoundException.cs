namespace NetStore.Shared.Abstractions.Exceptions;

public abstract class NotFoundException : ApiException
{
    public NotFoundException(string message) : base(message)
    {
    }
}