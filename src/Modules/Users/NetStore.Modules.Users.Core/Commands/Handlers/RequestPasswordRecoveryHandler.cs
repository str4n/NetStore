using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Users.Core.Commands.Handlers;

internal sealed class RequestPasswordRecoveryHandler : ICommandHandler<RequestPasswordRecovery>
{
    private readonly IUserRepository _repository;
    private readonly IMessageBroker _messageBroker;
    private readonly IRecoveryTokenRepository _tokenRepository;

    public RequestPasswordRecoveryHandler(IUserRepository repository, IMessageBroker messageBroker, 
        IRecoveryTokenRepository tokenRepository)
    {
        _repository = repository;
        _messageBroker = messageBroker;
        _tokenRepository = tokenRepository;
    }
    
    public async Task HandleAsync(RequestPasswordRecovery command)
    {
        var user = await _repository.GetByEmailAsync(command.Email);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        if (user.State != UserState.Active)
        {
            throw new UserNotActiveException(user.Username);
        }

        var token = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
        var recoveryToken = new RecoveryToken(token, user.Id);

        await _tokenRepository.AddAsync(recoveryToken);

        await _messageBroker.PublishAsync(new PasswordRecoverRequested(user.Email, user.Username, token));
    }
}