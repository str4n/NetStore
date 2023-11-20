﻿namespace NetStore.Shared.Infrastructure.Auth;

internal sealed class AuthOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }
    public TimeSpan Expiry { get; set; }
}