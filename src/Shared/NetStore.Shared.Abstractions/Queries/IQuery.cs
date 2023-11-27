using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Shared.Abstractions.Queries;

public interface IQuery : IMessage
{
}

public interface IQuery<TResult> : IQuery
{
}