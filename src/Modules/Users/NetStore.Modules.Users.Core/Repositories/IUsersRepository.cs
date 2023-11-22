using NetStore.Modules.Users.Core.Domain.Entities;
using NetStore.Modules.Users.Core.Domain.ValueObjects;

namespace NetStore.Modules.Users.Core.Repositories;

internal interface IUsersRepository
{
    Task<User> GetByUsernameAsync(Username username);
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetAsync(Guid id);
    Task AddAsync(User user);
}