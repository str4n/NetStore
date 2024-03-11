using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using NetStore.Shared.Abstractions.Types.ValueObjects;

namespace NetStore.Shared.Infrastructure.Auth.Policies.Handlers;

internal sealed class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        var currentRole = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        var requireAtLeastRole = requirement.Role.Value;

        var indexOfRequiredAtLeastRole = Array.IndexOf(Role.RoleHierarchy, requireAtLeastRole);
        var indexOfCurrentRole = Array.IndexOf(Role.RoleHierarchy, currentRole);

        if (indexOfCurrentRole >= indexOfRequiredAtLeastRole)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
        
        context.Fail();

        return Task.CompletedTask;
    }
}