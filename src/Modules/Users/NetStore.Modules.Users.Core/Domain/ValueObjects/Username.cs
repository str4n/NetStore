using NetStore.Modules.Users.Core.Domain.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.ValueObjects;

internal sealed record Username
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidUsernameException("Username cannot be empty.");
        }

        if (value.Length is > 30 or < 3)
        {
            throw new InvalidUsernameException("Username must be between 3 and 30 letters");
        }

        Value = value;
    }
    
    public static implicit operator string(Username username) => username.Value;
    public static implicit operator Username(string username) => new(username);
}