using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Customers.Core.EF;
using NetStore.Modules.Customers.Core.Messaging;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Customers.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEF(configuration);

        services
            .AddConsumer<CreateCustomerConsumer>()
            .AddConsumer<OrderPlacedConsumer>();
        
        return services;
    }
}