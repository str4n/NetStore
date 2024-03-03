using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Notifications.Core.Consumers;
using NetStore.Modules.Notifications.Core.Services;
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

        services.AddConsumer<AccountActivationRequestedConsumer>().AddConsumer<PasswordRecoverRequestedConsumer>();
        
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IUrlShortener, ExternalUrlShortener>();
        
        return services;
    }
}