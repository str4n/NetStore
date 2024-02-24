using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Shared.Types.ValueObjects;

namespace NetStore.Modules.Users.Core.Repositories;

internal interface IUserRepository
{
    Task<User> GetByUsernameAsync(Username username);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}