using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Notifications.Core;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Notifications.Api;

public sealed class NotificationsModule : Module
{
    public const string BasePath = "notifications-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore(configuration);
    }

    public override void UseModule(WebApplication app)
    {
    }
}