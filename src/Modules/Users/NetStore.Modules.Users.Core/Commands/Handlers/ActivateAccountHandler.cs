using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands.Handlers;

internal sealed class ActivateAccountHandler : ICommandHandler<ActivateAccount>
{
    private readonly IUserRepository _userRepository;
    private readonly IActivationTokenRepository _tokenRepository;

    public ActivateAccountHandler(IUserRepository userRepository, IActivationTokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
    }
    
    public async Task HandleAsync(ActivateAccount command)
    {
        var token = await _tokenRepository.GetAsync(command.ActivationToken);
        var userId = token.UserId;

        if (token is null)
        {
            throw new ActivationTokenNotFound();
        }
        
        var user = await _userRepository.GetAsync(userId);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.State = UserState.Active;

        await _userRepository.UpdateAsync(user);
        await _tokenRepository.DeleteAsync(token);
    }
}