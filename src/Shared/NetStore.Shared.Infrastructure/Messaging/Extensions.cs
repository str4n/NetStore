using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Shared.Infrastructure.Messaging;

internal static class Extensions
{
    private const string SectionName = "RabbitMq";
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<RabbitMqOptions>(section);

        services.AddMassTransit(busConfigurator =>
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName!.Contains("NetStore"));

            foreach (var assembly in assemblies)
            {
                busConfigurator.AddConsumers(assembly);
            }
            
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var options = configuration.GetOptions<RabbitMqOptions>(SectionName);
                
                configurator.Host(new Uri(options.Host), hostConfigurator =>
                {
                    hostConfigurator.Username(options.Username);
                    hostConfigurator.Password(options.Password);
                });
                
                configurator.ConfigureEndpoints(context);
            });
        });
        
        services.AddTransient<IMessageBroker, RabbitMqMessageBroker>();

        return services;
    }
}

public static class ConsumerExtensions
{
    public static IServiceCollection AddConsumer<TConsumer>(this IServiceCollection services) where TConsumer : class, IConsumer
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<TConsumer>();
        });

        return services;
    }
}