using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Customers.Core.Events;

internal sealed class UserCreatedEventHandler : IEventHandler<UserCreated>
{
    private readonly ICustomersRepository _customersRepository;

    public UserCreatedEventHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }
    
    public async Task HandleAsync(UserCreated @event)
    {
        var customer = Customer.CreateFromUser(@event.Id, @event.Email);

        await _customersRepository.AddAsync(customer);
    }
}