using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Shared.Infrastructure.Messaging;

public interface ICrossModuleQueryDispatcher
{
    Task<TResult> SendAsync<TResult>(ICrossModuleQuery<TResult> query);
}