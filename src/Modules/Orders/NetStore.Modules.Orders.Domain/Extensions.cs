using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Orders.Domain.Services;

namespace NetStore.Modules.Orders.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IOrderDomainService, OrderDomainService>();
        
        return services;
    }
}