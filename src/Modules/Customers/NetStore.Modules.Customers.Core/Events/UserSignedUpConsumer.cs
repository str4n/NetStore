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

    public UserSignedUpConsumer(ICustomersRepository customersRepository, ILogger<UserSignedUpConsumer> logger)
    {
        _customersRepository = customersRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserSignedUp> context)
    {
        var message = context.Message;
        var customer = Customer.CreateFromUser(message.Id, message.Email);
        
        await _customersRepository.AddAsync(customer);
        _logger.LogInformation("Customer created: {customer}", message);
    }
}

