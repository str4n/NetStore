using Microsoft.AspNetCore.Http;
using NetStore.Shared.Abstractions.Auth;

namespace NetStore.Shared.Infrastructure.Auth;

internal sealed class HttpContextTokenStorage : ITokenStorage
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(JsonWebToken token) => _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, token);

    public JsonWebToken Get()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }
        
        if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var token))
        {
            return token as JsonWebToken;
        }

        return null;
    }
    
}