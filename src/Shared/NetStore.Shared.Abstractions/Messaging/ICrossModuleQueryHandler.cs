namespace NetStore.Shared.Abstractions.Messaging;

public interface ICrossModuleQueryHandler<in TQuery, TResult> where TQuery : ICrossModuleQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery query);
}