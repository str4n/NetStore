using MassTransit;
using Microsoft.Extensions.Logging;
using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Modules.Users.Shared.Events;

namespace NetStore.Modules.Customers.Core.Events;

internal sealed class UserSignedUpConsumer : IConsumer<UserSignedUp>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<UserSignedUpConsumer> _logger;

    public UserSignedUpConsumer(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task Consume(ConsumeContext<UserSignedUp> context)
    {
        var message = context.Message;
        var customer = Customer.CreateFromUser(message.Id, message.Email);
        
        await _customerRepository.AddAsync(customer);
    }
}

