using Chronicle;
using NetStore.Modules.Notifications.Shared.Commands;
using NetStore.Modules.Users.Shared.Commands;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Saga.Sagas;

internal sealed class PasswordRecoverySaga : Saga<PasswordRecoveryData>, ISagaStartAction<PasswordRecoveryRequested>, 
    ISagaAction<PasswordRecoveryPrepared>, ISagaAction<PasswordRecovered>
{
    private readonly IMessageBroker _messageBroker;

    public PasswordRecoverySaga(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            PasswordRecoveryRequested m => m.UserId.ToString(),
            PasswordRecoveryPrepared m => m.UserId.ToString(),
            PasswordRecovered m => m.UserId.ToString(),
            _ => base.ResolveId(message, context)
        };

    public async Task HandleAsync(PasswordRecoveryRequested message, ISagaContext context)
    {
        var (id, email, username) = message;

        Data.UserId = id;
        Data.Email = email;
        Data.Username = username;

        await _messageBroker.PublishAsync(new PreparePasswordRecovery(Data.UserId));
    }

    public async Task HandleAsync(PasswordRecoveryPrepared message, ISagaContext context)
    {
        Data.RecoveryToken = message.Token;

        await _messageBroker.PublishAsync(new SendRecoveryEmail(Data.Email, Data.Username, Data.RecoveryToken));
    }
    
    public async Task HandleAsync(PasswordRecovered message, ISagaContext context)
    {
        await CompleteAsync();
    }
    
    public Task CompensateAsync(PasswordRecoveryRequested message, ISagaContext context)
        => Task.CompletedTask;

    public Task CompensateAsync(PasswordRecoveryPrepared message, ISagaContext context)
        => Task.CompletedTask;

    public Task CompensateAsync(PasswordRecovered message, ISagaContext context)
        => Task.CompletedTask;
}

internal class PasswordRecoveryData
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string RecoveryToken { get; set; }
}