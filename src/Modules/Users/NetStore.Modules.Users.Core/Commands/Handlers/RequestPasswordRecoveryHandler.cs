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

    public RequestPasswordRecoveryHandler(IUserRepository repository, IMessageBroker messageBroker)
    {
        _repository = repository;
        _messageBroker = messageBroker;
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

        await _messageBroker.PublishAsync(new PasswordRecoveryRequested(user.Id, user.Email, user.Username));
    }
}