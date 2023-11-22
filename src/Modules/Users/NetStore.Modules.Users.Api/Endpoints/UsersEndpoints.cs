using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Users.Core.CQRS.Commands;
using NetStore.Modules.Users.Core.CQRS.Queries;
using NetStore.Modules.Users.Core.DTO;
using NetStore.Shared.Abstractions.Auth;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Users.Api.Endpoints;

internal static class UsersEndpoints
{
    private const string Route = UsersModule.BasePath + "/users";

    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, Get).RequireAuthorization();

        app.MapPost(Route + "/sign-up", SignUp);
        app.MapPost(Route + "/sign-in", SignIn);

        return app;
    }

    private static async Task<IResult> Get([FromServices]IIdentityContext identityContext, [FromServices]IQueryDispatcher queryDispatcher)
    {
        var id = identityContext.Id;

        var result = await queryDispatcher.SendAsync<GetUser, UserDto>(new GetUser(id));

        if (result is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(result);
    }

    private static async Task<IResult> SignUp([FromBody]SignUp command, [FromServices]ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command with { Id = Guid.NewGuid()});

        return Results.Ok();
    }

    private static async Task<IResult> SignIn([FromBody]SignIn command, [FromServices]ICommandDispatcher commandDispatcher, [FromServices]ITokenStorage tokenStorage)
    {
        await commandDispatcher.SendAsync(command);
        var token = tokenStorage.Get();
        
        return Results.Ok(token);
    }
}