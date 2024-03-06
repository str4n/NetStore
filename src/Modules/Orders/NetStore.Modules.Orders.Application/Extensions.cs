using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Orders.Application.Events;
using NetStore.Modules.Orders.Application.Messaging;
using NetStore.Modules.Orders.Application.PaymentRegistry;
using NetStore.Modules.Orders.Application.Storage;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Orders.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddConsumer<ProductCreatedConsumer>()
            .AddConsumer<CreateCartConsumer>()
            .AddConsumer<OrderPlacedConsumer>()
            .AddConsumer<ProductStockQuantityIncreasedConsumer>()
            .AddConsumer<ProductStockQuantityDecreasedConsumer>()
            .AddConsumer<PaymentCompletedConsumer>()
            .AddConsumer<PaymentRequestedConsumer>();

        services.AddSingleton<IPaymentRegistry, InMemoryPaymentRegistry>();
        services.AddScoped<IPaymentStorage, HttpContextPaymentStorage>();
        
        return services;
    }
}