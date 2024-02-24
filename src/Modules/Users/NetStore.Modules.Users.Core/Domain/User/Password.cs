using NetStore.Modules.Users.Core.Domain.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.User;

internal sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPasswordSyntaxException("Password cannot be empty.");
        }
        

        Value = value;
    }

    public static implicit operator string(Password password) => password.Value;
    public static implicit operator Password(string password) => new(password);
}