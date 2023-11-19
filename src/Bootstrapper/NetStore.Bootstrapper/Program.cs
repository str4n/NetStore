using NetStore.Bootstrapper;
using NetStore.Modules.Products.Api;
using NetStore.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

ModuleLoader.Load<ProductsModule>();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddModules(builder.Configuration);

var app = builder.Build();

app.UseModules();

app.UseInfrastructure();

app.MapControllers();

app.Run();