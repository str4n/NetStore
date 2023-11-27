namespace NetStore.Shared.Abstractions.Messaging;

public interface ICrossModuleQuery : IMessage
{
}

public interface ICrossModuleQuery<TResult> : ICrossModuleQuery
{
}