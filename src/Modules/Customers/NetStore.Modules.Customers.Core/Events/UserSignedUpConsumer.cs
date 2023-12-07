using MassTransit;
using Microsoft.Extensions.Logging;
using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Modules.Users.Shared.Events;

namespace NetStore.Modules.Customers.Core.Events;

internal sealed class UserSignedUpConsumer : IConsumer<UserSignedUp>
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ILogger<UserSignedUpConsumer> _logger;

    public UserSignedUpConsumer(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task Consume(ConsumeContext<UserSignedUp> context)
    {
        var message = context.Message;
        var customer = Customer.CreateFromUser(message.Id, message.Email);
        
        await _customersRepository.AddAsync(customer);
    }
}

