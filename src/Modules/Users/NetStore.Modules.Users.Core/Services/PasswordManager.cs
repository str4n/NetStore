using Microsoft.AspNetCore.Identity;
using NetStore.Modules.Users.Core.Domain.User;

namespace NetStore.Modules.Users.Core.Services;

internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User>_passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password) => _passwordHasher.HashPassword(default!, password);

    public bool Validate(string password, string securedPassword)
        => _passwordHasher.VerifyHashedPassword(default!, securedPassword, password) 
            is PasswordVerificationResult.Success;
}