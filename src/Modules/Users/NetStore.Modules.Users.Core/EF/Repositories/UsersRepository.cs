using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Users.Core.Domain.Entities;
using NetStore.Modules.Users.Core.Domain.ValueObjects;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Types.SharedTypes.ValueObjects;

namespace NetStore.Modules.Users.Core.EF.Repositories;

internal sealed class UsersRepository : IUsersRepository
{
    private readonly UsersDbContext _dbContext;
    private readonly DbSet<User> _users;

    public UsersRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
        _users = dbContext.Users;
    }

    public Task<User> GetByUsernameAsync(Username username)
    {
        username = username.Value.ToLowerInvariant();
        
        return _users.SingleOrDefaultAsync(x => x.Username == username);
    }

    public Task<User> GetByEmailAsync(Email email)
    {
        email = email.Value.ToLowerInvariant();
        
        return _users.SingleOrDefaultAsync(x => x.Email == email);
    }

    public Task<User> GetAsync(Guid id)
        => _users.SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
    }

    public Task UpdateAsync(User user)
    {
        _users.Update(user);
        return Task.CompletedTask;
    }
}