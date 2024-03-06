using Chronicle;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Saga;

internal static class Extensions
{
    public static IServiceCollection AddSaga(this IServiceCollection services)
    {
        services.AddChronicle();

        services.AddConsumer<MessagesHandler>();
        
        return services;
    }
}