using NetStore.Bootstrapper;
using NetStore.Modules.Catalogs.Api;
using NetStore.Modules.Customers.Api;
using NetStore.Modules.Users.Api;
using NetStore.Shared.Infrastructure;
using NetStore.Shared.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

ModuleLoader.Load<CatalogsModule>();
ModuleLoader.Load<UsersModule>();
ModuleLoader.Load<CustomersModule>();

builder.Host.UseLogging();

builder.Services
    .AddModules(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseModules();

app.UseInfrastructure();

app.MapControllers();

app.Run();