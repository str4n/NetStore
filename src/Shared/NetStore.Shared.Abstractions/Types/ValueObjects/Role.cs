using NetStore.Shared.Abstractions.Types.Exceptions;

namespace NetStore.Shared.Abstractions.Types.ValueObjects;

public sealed record Role
{
    public static readonly string[] RoleHierarchy = { User, Employee, Admin }; // the first role has the least permissions
    private static readonly string[] AvailableRoles = { Admin, User, Employee };
    
    public const string User = "user";
    public const string Admin = "admin";
    public const string Employee = "employee";
    
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