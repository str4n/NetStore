using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Notifications.Core.Facades;
using NetStore.Modules.Notifications.Core.Messaging;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Notifications.Core;

public static class Extensions
{
    private const string EmailSenderSection = "EmailSender";
    private const string UrlShortenerSection = "UrlShortenerIntegration";
    
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSenderOptions>(configuration.GetSection(EmailSenderSection));
        services.Configure<ExternalUrlShortenerOptions>(configuration.GetSection(UrlShortenerSection));

        services
            .AddConsumer<SendActivationEmailConsumer>()
            .AddConsumer<PasswordRecoverRequestedConsumer>()
            .AddConsumer<OrderPlacedConsumer>();
        
        services.AddTransient<IEmailService, EmailService>();

        services
            .AddTransient<IEmailSenderFacade, EmailSenderFacade>()
            .AddTransient<IUrlShortenerFacade, ExternalUrlShortener>();
        
        return services;
    }
}