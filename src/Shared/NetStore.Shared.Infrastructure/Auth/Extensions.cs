using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetStore.Shared.Abstractions.Auth;
using NetStore.Shared.Infrastructure.Auth.Policies;
using NetStore.Shared.Infrastructure.Auth.Policies.Handlers;
using NetStore.Shared.Types.ValueObjects;

namespace NetStore.Shared.Infrastructure.Auth;

internal static class Extensions
{
    private const string SectionName = "Auth";
    
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetRequiredSection(SectionName));
        
        var options = configuration.GetOptions<AuthOptions>(SectionName);

        services.AddSingleton<ITokenStorage, HttpContextTokenStorage>();
        services.AddSingleton<IAuthenticator, Authenticator>()
            .AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.Audience = options.Audience;
            opt.IncludeErrorDetails = true;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
            };
        });

        services.AddScoped<IAuthorizationHandler, RoleRequirementHandler>();

        services.AddAuthorization(authorizationOptions =>
        {
            authorizationOptions.AddPolicy(Policies.Policies.AtLeastEmployee, builder => builder.AddRequirements(new RoleRequirement(Role.Employee)));
            authorizationOptions.AddPolicy(Policies.Policies.AtLeastAdmin, builder => builder.AddRequirements(new RoleRequirement(Role.Admin)));
        });

        return services;
    }
}