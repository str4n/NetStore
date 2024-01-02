namespace NetStore.Shared.Abstractions.Modules.Requests;

public interface IModuleRequestDispatcher
{
    Task<TResult> SendAsync<TResult>(IModuleRequest request);
}