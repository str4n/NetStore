using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Catalogs.Domain.Services;

namespace NetStore.Modules.Catalogs.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddSingleton<IProductDomainService, ProductDomainService>();

        return services;
    }
}