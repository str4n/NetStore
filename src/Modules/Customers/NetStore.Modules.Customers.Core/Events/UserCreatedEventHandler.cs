using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Events;

namespace NetStore.Modules.Customers.Core.Events;

internal sealed class UserSignedUpHandler : IEventHandler<UserSignedUp>
{
    private readonly ICustomersRepository _customersRepository;

    public UserSignedUpHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task HandleAsync(UserSignedUp @event)
    {
        var customer = Customer.CreateFromUser(@event.Id, @event.Email);

        await _customersRepository.AddAsync(customer);
    }
}

