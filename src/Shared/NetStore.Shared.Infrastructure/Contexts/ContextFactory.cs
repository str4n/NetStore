using Microsoft.AspNetCore.Http;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Shared.Infrastructure.Contexts;

internal sealed class ContextFactory : IContextFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ContextFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public IIdentityContext Create()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        return httpContext is not null ? new IdentityContext(httpContext.User) : IdentityContext.Empty;
    }
}