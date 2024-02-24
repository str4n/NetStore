using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Repositories;

namespace NetStore.Modules.Users.Core.EF.Repositories;

internal sealed class ActivationTokenRepository : IActivationTokenRepository
{
    private readonly UsersDbContext _dbContext;

    public ActivationTokenRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ActivationToken> GetAsync(string secret)
        => _dbContext.ActivationTokens.SingleOrDefaultAsync(x => x.Secret == secret);

    public async Task DeleteAsync(ActivationToken token)
    {
        _dbContext.ActivationTokens.Remove(token);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(ActivationToken token)
    {
        await _dbContext.ActivationTokens.AddAsync(token);
        await _dbContext.SaveChangesAsync();
    }
}