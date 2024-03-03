using NetStore.Modules.Users.Core.Domain.User;

namespace NetStore.Modules.Users.Core.Repositories;

public interface IActivationTokenRepository
{
    public Task<ActivationToken> GetAsync(string token);
    public Task DeleteAsync(ActivationToken token);
    public Task AddAsync(ActivationToken token);
}