using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Abstractions.Types.ValueObjects;

namespace NetStore.Modules.Users.Core.EF.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;
    private readonly DbSet<User> _users;

    public UserRepository(UsersDbContext dbContext)
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
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}