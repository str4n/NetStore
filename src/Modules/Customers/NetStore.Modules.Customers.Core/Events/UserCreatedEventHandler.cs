using NetStore.Modules.Customers.Core.Domain.Entities;
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
        var customer = new Customer(Guid.NewGuid(), @event.Email, @event.Id);

        await _customersRepository.AddAsync(customer);
    }
}