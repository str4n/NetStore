﻿using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Orders.Application.Events;
using NetStore.Modules.Orders.Application.Storage;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Orders.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddConsumer<ProductCreatedConsumer>()
            .AddConsumer<UserSignedUpConsumer>()
            .AddConsumer<OrderPlacedConsumer>();

        services.AddScoped<IPaymentStorage, HttpContextPaymentStorage>();
        
        return services;
    }
}