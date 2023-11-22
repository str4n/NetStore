namespace NetStore.Shared.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResult> SendAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
}