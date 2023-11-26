using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Customers.Core.CQRS.Commands;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Customers.Api.Endpoints;

internal static class CustomerEndpoints
{
    private const string Route = CustomersModule.BasePath + "/customers";
    
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPut(Route, Put).RequireAuthorization();
        
        return app;
    }

    [Authorize]
    private static async Task<IResult> Put([FromBody]CompleteCustomerInformation command,
        [FromServices]ICommandDispatcher commandDispatcher, [FromServices]IIdentityContext identityContext)
    {
        var id = identityContext.Id;
        await commandDispatcher.SendAsync(command with { Id = id });

        return Results.NoContent();
    }
}