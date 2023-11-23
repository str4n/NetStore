using System.Text.RegularExpressions;
using NetStore.Modules.Users.Core.Domain.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.ValueObjects;

internal sealed record Password
{
    private static readonly Regex Regex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
    
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPasswordException("Password cannot be empty.");
        }


        if (!Regex.IsMatch(value))
        {
            throw new InvalidPasswordException("Password must contain minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.");
        }

        Value = value;
    }

    public static implicit operator string(Password password) => password.Value;
    public static implicit operator Password(string password) => new(password);
}