using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Repositories;

namespace NetStore.Modules.Users.Core.EF.Repositories;

internal sealed class RecoveryTokenRepository : IRecoveryTokenRepository
{
    private readonly UsersDbContext _dbContext;

    public RecoveryTokenRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<RecoveryToken> GetAsync(string token)
        => _dbContext.RecoveryTokens.SingleOrDefaultAsync(x => x.Token == token);

    public async Task DeleteAsync(RecoveryToken token)
    {
        _dbContext.RecoveryTokens.Remove(token);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddAsync(RecoveryToken token)
    {
        await _dbContext.RecoveryTokens.AddAsync(token);
        await _dbContext.SaveChangesAsync();
    }
}