using Microsoft.AspNetCore.Authorization;
using NetStore.Shared.Abstractions.Types.ValueObjects;

namespace NetStore.Shared.Infrastructure.Auth.Policies;

internal sealed class RoleRequirement : IAuthorizationRequirement
{
    public Role Role { get; private set; }

    public RoleRequirement(Role role)
    {
        Role = role;
    }
}