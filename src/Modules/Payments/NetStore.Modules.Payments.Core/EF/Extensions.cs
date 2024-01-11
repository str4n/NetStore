using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Payments.Core.EF;

internal static class Extensions
{
    public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres<PaymentsDbContext>(configuration);
        
        return services;
    }
}