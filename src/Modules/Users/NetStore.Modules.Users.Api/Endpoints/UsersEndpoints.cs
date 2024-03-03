using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Users.Core.Commands;
using NetStore.Modules.Users.Core.DTO;
using NetStore.Modules.Users.Core.Queries;
using NetStore.Shared.Abstractions.Auth;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;
using NetStore.Shared.Abstractions.Queries;
using NetStore.Shared.Infrastructure.Auth.Policies;
using NetStore.Shared.Types.ValueObjects;

namespace NetStore.Modules.Users.Api.Endpoints;

internal static class UsersEndpoints
{
    private const string Route = UsersModule.BasePath + "/users";

    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, Get).RequireAuthorization();

        app.MapPost(Route + "/sign-up", SignUp);
        app.MapPost(Route + "/sign-in", SignIn);

        app.MapPut(Route + "/activate/{secret}", Activate);
        app.MapPut(Route + "/recover", RequestPasswordRecovery);
        app.MapPost(Route + "/recover/{recoveryToken}", RecoverPassword);

        app.MapDelete(Route + "/{id:guid}", Delete).RequireAuthorization(Policies.AtLeastAdmin);

        return app;
    }

    private static async Task<IResult> Get([FromServices]IIdentityContext identityContext, [FromServices]IQueryDispatcher queryDispatcher)
    {
        var id = identityContext.Id;

        var result = await queryDispatcher.SendAsync<GetUser, UserDto>(new GetUser(id));

        return result is null ? Results.NotFound() : Results.Ok(result);
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

    private static async Task<IResult> Activate([FromRoute] string secret, [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(new ActivateAccount(secret));

        return Results.Ok();
    }
    
    private static async Task<IResult> Delete([FromRoute]Guid id, [FromServices]ICommandDispatcher commandDispatcher)
    {
        var command = new DeleteUser(id);

        await commandDispatcher.SendAsync(command);

        return Results.NoContent();
    }

    private static async Task<IResult> RequestPasswordRecovery([FromBody] RequestPasswordRecovery command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command);

        return Results.Ok();
    }
    
    private static async Task<IResult> RecoverPassword([FromRoute] string recoveryToken,[FromBody] RecoverPassword command,
        [FromServices] ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(command with { RecoveryToken = recoveryToken });

        return Results.Ok();
    }
    
}