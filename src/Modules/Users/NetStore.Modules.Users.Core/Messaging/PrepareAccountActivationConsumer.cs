using MassTransit;
using NetStore.Modules.Notifications.Shared.Commands;
using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Shared.Commands;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Users.Core.Messaging;

internal sealed class PrepareAccountActivationConsumer : IConsumer<PrepareAccountActivation>
{
    private readonly IActivationTokenRepository _tokenRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IUserRepository _userRepository;

    public PrepareAccountActivationConsumer(IActivationTokenRepository tokenRepository, IMessageBroker messageBroker, 
        IUserRepository userRepository)
    {
        _tokenRepository = tokenRepository;
        _messageBroker = messageBroker;
        _userRepository = userRepository;
    }
    
    public async Task Consume(ConsumeContext<PrepareAccountActivation> context)
    {
        var id = context.Message.UserId;

        var user = await _userRepository.GetAsync(id);

        if (user is null)
        {
            return;
        }
        
        var token = Guid.NewGuid().ToString();
        var activationToken = new ActivationToken(token, id);

        await _tokenRepository.AddAsync(activationToken);
        await _messageBroker.PublishAsync(new AccountActivationPrepared(user.Id, token));
    }
}