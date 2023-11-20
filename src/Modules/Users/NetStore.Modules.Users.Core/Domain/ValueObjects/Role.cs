using NetStore.Modules.Users.Core.Domain.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.ValueObjects;

internal sealed record Role
{
    private static readonly string[] AvailableRoles = { Admin, User };
    
    public const string User = "user";
    public const string Admin = "admin";
    
    public string Value { get; }

    public Role(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
        {
            Value = User;
        }

        if (!AvailableRoles.Contains(role))
        {
            throw new InvalidRoleException();
        }

        Value = role;
    }

    public static implicit operator string(Role role) => role.Value;
    public static implicit operator Role(string role) => new(role);
}