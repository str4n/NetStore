using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Shared.Abstractions.Modules;

namespace NetStore.Modules.Catalogs.Api;

public sealed class CatalogsModule : Module
{
    public const string BasePath = "catalogs-module";
    public override string Path => BasePath;
    public override void AddModule(IServiceCollection services, IConfiguration configuration)
    {
    }

    public override void UseModule(WebApplication app)
    {
    }
}