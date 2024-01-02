namespace NetStore.Shared.Abstractions.Modules.Requests;

public interface IModuleRequestHandler<in TRequest, TResult> where TRequest : class, IModuleRequest<TResult>
{
    Task<TResult> HandleAsync(TRequest request);
}