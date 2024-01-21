using Microsoft.Extensions.Options;
using NetStore.Shared.Infrastructure.Auth;
using NetStore.Shared.Infrastructure.Time;

namespace NetStore.Tests.Shared.Integration;

public static class AuthHelper
{
    private static readonly Authenticator Authenticator;

    static AuthHelper()
    {
        var options = new OptionsProvider().GetOptions<AuthOptions>("Auth");
        Authenticator = new Authenticator(new UtcClock(), Options.Create(options));
    }

    public static string GenerateJwt(Guid userId, string role, string email) =>
        Authenticator.CreateToken(userId, role, email).AccessToken;
}