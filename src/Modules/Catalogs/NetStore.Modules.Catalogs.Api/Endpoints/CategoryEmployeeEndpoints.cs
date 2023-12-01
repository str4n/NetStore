using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Catalogs.Application.Commands;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Infrastructure.Auth.Policies;

namespace NetStore.Modules.Catalogs.Api.Endpoints;

internal static class CategoryEmployeeEndpoints
{
    private const string Route = CatalogsModule.BasePath + "/categories";
    
    public static IEndpointRouteBuilder MapCategoryEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(Route, Create).RequireAuthorization(Policies.AtLeastEmployee);
        
        return app;
    }
    
    private static async Task<IResult> Create([FromBody] CreateCategory command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.Created($"https://localhost:7240/{Route}", default);
    }
}