using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Customers.Core.EF;
using NetStore.Modules.Customers.Core.Events;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Customers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEF(configuration);
        
        return services;
    }
}