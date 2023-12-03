using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Catalogs.Application.Commands;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Infrastructure.Auth.Policies;

namespace NetStore.Modules.Catalogs.Api.Endpoints.ProductMockup;

internal static class ProductMockupAdminEndpoints
{
    private const string Route = CatalogsModule.BasePath + "/mockups";
    
    public static IEndpointRouteBuilder MapProductMockupAdminEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(Route, Create).RequireAuthorization(Policies.AtLeastAdmin);
        
        return app;
    }

    private static async Task<IResult> Create([FromBody] CreateProductMockup command, 
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.NoContent();
    }
}