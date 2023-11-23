using NetStore.Bootstrapper;
using NetStore.Modules.Customers.Api;
using NetStore.Modules.Products.Api;
using NetStore.Modules.Users.Api;
using NetStore.Shared.Infrastructure;
using NetStore.Shared.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

ModuleLoader.Load<ProductsModule>();
ModuleLoader.Load<UsersModule>();
ModuleLoader.Load<CustomersModule>();

builder.Host.UseLogging();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddModules(builder.Configuration);

var app = builder.Build();

app.UseModules();

app.UseInfrastructure();

app.MapControllers();

app.Run();