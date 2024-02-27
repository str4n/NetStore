using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.EF;
using UrlShortener.Request;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("Postgres")["ConnectionString"];
builder.Services.AddDbContext<UrlDbContext>(x => x.UseNpgsql(connectionString));

builder.Services.AddScoped<IUrlService, UrlService>();

var app = builder.Build();

app.MapGet("/{code}", async ([FromRoute]string code, [FromServices]IUrlService urlService) =>
{
    var result = await urlService.Get(code);

    return Results.Redirect(result.LongUrl);
});

app.MapPost("/", async ([FromBody] ShortenUrlRequest request, [FromServices] IUrlService urlService) =>
{
    var result = await urlService.Shorten(request);

    return Results.Ok(result);
});

app.Run();