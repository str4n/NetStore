using NetStore.Shared.Abstractions.Types.ValueObjects;

namespace NetStore.Modules.Users.Core.Domain.User;

internal sealed class User
{
    public Guid Id { get; set; }
    public Email Email { get; set; }
    public Username Username { get; set; }
    public Password Password { get; set; }
    public Role Role { get; set; }
    public UserState State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User(Guid id, Email email, Username username, Password password, Role role, UserState state, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        State = state;
        CreatedAt = createdAt;
    }
}

public enum UserState
{
    Deleted,
    NotActive,
    Active
}