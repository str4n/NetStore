using NetStore.Modules.Users.Core.Domain.User;

namespace NetStore.Modules.Users.Core.Repositories;

internal interface IRecoveryTokenRepository
{
    public Task<RecoveryToken> GetAsync(string token);
    public Task DeleteAsync(RecoveryToken token);
    public Task AddAsync(RecoveryToken token);
}