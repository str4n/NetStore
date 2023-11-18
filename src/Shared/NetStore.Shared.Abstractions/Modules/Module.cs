using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetStore.Shared.Abstractions.Modules;

public abstract class Module
{
    public string Name => GetType().Name;
    public string Path => GetType().Name.Replace("Module", "-module").ToLower();

    public abstract void AddModule(IServiceCollection services, IConfiguration configuration);
    public abstract void UseModule(WebApplication app);
}