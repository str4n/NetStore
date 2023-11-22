using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Shared.Infrastructure.Contexts;

public interface IContextFactory
{
    IIdentityContext Create();
}