using NetStore.Bootstrapper;
using NetStore.Modules.Products.Api;
using NetStore.Shared.Infrastructure;

ModuleLoader.RegisterModule<ProductsModule>();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddModules(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseModules();

app.UseInfrastructure();

app.Run();