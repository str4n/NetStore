using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Catalogs.Application.Commands;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Infrastructure.Auth.Policies;

namespace NetStore.Modules.Catalogs.Api.Endpoints.Category;

internal static class CategoryAdminEndpoints
{
    private const string Route = CatalogsModule.BasePath + "/categories";
    
    public static IEndpointRouteBuilder MapCategoryAdminEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(Route, Create).RequireAuthorization(Policies.AtLeastAdmin);
        app.MapPut(Route + "/{id:long}", Update).RequireAuthorization(Policies.AtLeastAdmin);
        app.MapDelete(Route + "/{id:long}", Delete).RequireAuthorization(Policies.AtLeastAdmin);
        
        return app;
    }
    
    private static async Task<IResult> Create([FromBody] CreateCategory command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.Created($"https://localhost:7240/{Route}", default);
    }

    private static async Task<IResult> Update([FromRoute] long id, [FromBody] UpdateCategory command, 
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command with { Id = id });

        return Results.NoContent();
    }

    private static async Task<IResult> Delete([FromRoute] long id, [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(new DeleteCategory(id));

        return Results.NoContent();
    }
}