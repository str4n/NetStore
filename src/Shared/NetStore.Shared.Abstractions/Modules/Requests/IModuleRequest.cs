using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Shared.Abstractions.Modules.Requests;

public interface IModuleRequest : IMessage
{
}

public interface IModuleRequest<TResult> : IModuleRequest
{
}