using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Users.Api.Endpoints;
using NetStore.Modules.Users.Core;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Users.Api;

public sealed class UsersModule : Module
{
    public const string BasePath = "users-module";

    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCore(configuration);
    }

    public override void UseModule(WebApplication app)
    {
        app.MapUsersEndpoints();
    }
}