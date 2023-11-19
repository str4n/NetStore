using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetStore.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}