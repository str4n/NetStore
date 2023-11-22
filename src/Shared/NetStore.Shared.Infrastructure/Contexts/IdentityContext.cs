using System.Security.Claims;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Shared.Infrastructure.Contexts;

internal sealed class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string Role { get; }

    public IdentityContext(ClaimsPrincipal claimsPrincipal)
    {
        IsAuthenticated = claimsPrincipal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(claimsPrincipal.Identity?.Name!) : Guid.Empty;
        Role = claimsPrincipal.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
    }

    private IdentityContext()
    {
    }

    public static IIdentityContext Empty => new IdentityContext();
}