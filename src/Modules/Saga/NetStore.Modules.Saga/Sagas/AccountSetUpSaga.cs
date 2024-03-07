using Chronicle;
using NetStore.Modules.Customers.Shared.Commands;
using NetStore.Modules.Customers.Shared.Events;
using NetStore.Modules.Notifications.Shared.Commands;
using NetStore.Modules.Orders.Shared.Commands;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Modules.Users.Shared.Commands;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Saga.Sagas;

internal sealed class AccountSetUpSaga : Saga<AccountSetupData>, ISagaStartAction<UserSignedUp>, ISagaAction<CustomerCreated>, 
   ISagaAction<CartCreated>, ISagaAction<AccountActivationPrepared>, ISagaAction<AccountActivated>
{
    private readonly IMessageBroker _messageBroker;

    public override SagaId ResolveId(object message, ISagaContext context)
        => message switch
        {
            UserSignedUp m => m.UserId.ToString(),
            CustomerCreated m => m.UserId.ToString(),
            CartCreated m => m.UserId.ToString(),
            AccountActivationPrepared m => m.UserId.ToString(),
            AccountActivated m => m.UserId.ToString(),
            _ => base.ResolveId(message, context)
        };

    public AccountSetUpSaga(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }
    
    public async Task HandleAsync(UserSignedUp message, ISagaContext context)
    {
        var (id, email, username) = message;

        Data.UserId = id;
        Data.Email = email;
        Data.Username = username;

        await _messageBroker.PublishAsync(new CreateCustomer(Data.UserId, Data.Email ));
    }
    
    public async Task HandleAsync(CustomerCreated message, ISagaContext context)
    {
        Data.CustomerCreated = true;

        await _messageBroker.PublishAsync(new CreateCart(Data.UserId));
    }
    
    public async Task HandleAsync(CartCreated message, ISagaContext context)
    {
        Data.CartCreated = true;
        
        await _messageBroker.PublishAsync(new PrepareAccountActivation(Data.UserId));
    }
    
    public async Task HandleAsync(AccountActivationPrepared message, ISagaContext context)
    {
        Data.ActivationToken = message.Token;

        await _messageBroker.PublishAsync(new SendActivationEmail(Data.Email, Data.Username, Data.ActivationToken));
    }
    
    public async Task HandleAsync(AccountActivated message, ISagaContext context)
    {
        Data.AccountActivated = true;

        await CompleteAsync();
    }
    
    public Task CompensateAsync(UserSignedUp message, ISagaContext context)
        => Task.CompletedTask;

    public Task CompensateAsync(CustomerCreated message, ISagaContext context)
        => Task.CompletedTask;

    public Task CompensateAsync(CartCreated message, ISagaContext context)
        => Task.CompletedTask;

    public Task CompensateAsync(AccountActivationPrepared message, ISagaContext context)
        => Task.CompletedTask;

    public Task CompensateAsync(AccountActivated message, ISagaContext context)
        => Task.CompletedTask;
}

internal class AccountSetupData
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string ActivationToken { get; set; }
    public bool CustomerCreated { get; set; }
    public bool CartCreated { get; set; }
    public bool AccountActivated { get; set; }
}