using MassTransit;
using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Shared.Commands;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Users.Core.Messaging;

internal sealed class PreparePasswordRecoveryConsumer : IConsumer<PreparePasswordRecovery>
{
    private readonly IRecoveryTokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMessageBroker _messageBroker;

    public PreparePasswordRecoveryConsumer(IRecoveryTokenRepository tokenRepository, IUserRepository userRepository, IMessageBroker messageBroker)
    {
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        _messageBroker = messageBroker;
    }
    
    

    public async Task Consume(ConsumeContext<PreparePasswordRecovery> context)
    {
        var id = context.Message.UserId;

        var user = await _userRepository.GetAsync(id);

        if (user is null)
        {
            return;
        }
        
        // TODO: Token hash

        var token = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
        var recoveryToken = new RecoveryToken(token, id);

        await _tokenRepository.AddAsync(recoveryToken);
        await _messageBroker.PublishAsync(new PasswordRecoveryPrepared(user.Id, token));
    }
}