using NetStore.Modules.Users.Core.Domain.ValueObjects;

namespace NetStore.Modules.Users.Core.Domain.Entities;

internal sealed class User
{
    public Guid Id { get; set; }
    public Email Email { get; set; }
    public Username Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public UserState UserState { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User(Guid id, Email email, Username username, string password, Role role, UserState userState, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
        UserState = userState;
        CreatedAt = createdAt;
    }
}

public enum UserState
{
    Deleted = 0,
    Active = 1
}