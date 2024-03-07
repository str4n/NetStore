using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Users.Core.Commands.Handlers;

internal sealed class ActivateAccountHandler : ICommandHandler<ActivateAccount>
{
    private readonly IUserRepository _userRepository;
    private readonly IActivationTokenRepository _tokenRepository;
    private readonly IMessageBroker _messageBroker;

    public ActivateAccountHandler(IUserRepository userRepository, IActivationTokenRepository tokenRepository, IMessageBroker messageBroker)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _messageBroker = messageBroker;
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
        await _messageBroker.PublishAsync(new AccountActivated(user.Id));
    }
}