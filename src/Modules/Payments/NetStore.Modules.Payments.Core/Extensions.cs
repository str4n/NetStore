using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Payments.Core.EF;
using NetStore.Modules.Payments.Core.Events;
using NetStore.Modules.Payments.Core.Services;
using NetStore.Modules.Payments.Core.Validators;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Payments.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddConsumer<PaymentRequestedConsumer>();
        
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<PaymentValidator>();
        services.AddEF(configuration);
        
        return services;
    }
}