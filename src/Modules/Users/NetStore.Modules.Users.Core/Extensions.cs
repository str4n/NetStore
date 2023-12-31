﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetStore.Modules.Users.Core.Domain.Entities;
using NetStore.Modules.Users.Core.EF;
using NetStore.Modules.Users.Core.Services;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Infrastructure.Attributes;

namespace NetStore.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEF(configuration);
        services
            .AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>()
            .AddSingleton<IPasswordManager, PasswordManager>();
        
        
        return services;
    }
}