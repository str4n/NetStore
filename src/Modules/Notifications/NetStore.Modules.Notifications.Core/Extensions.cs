using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Notifications.Core.Consumers;
using NetStore.Modules.Notifications.Core.Services;
using NetStore.Shared.Infrastructure.Messaging;

namespace NetStore.Modules.Notifications.Core;

public static class Extensions
{
    private const string SectionName = "EmailSender";
    
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSenderOptions>(configuration.GetSection(SectionName));

        services.AddConsumer<UserSignedUpConsumer>();
        
        services.AddTransient<IEmailService, EmailService>();
        
        return services;
    }
}