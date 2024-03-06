using MassTransit;
using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Modules.Customers.Shared.Commands;
using NetStore.Modules.Customers.Shared.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Customers.Core.Messaging;

internal sealed class CreateCustomerConsumer : IConsumer<CreateCustomer>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMessageBroker _messageBroker;

    public CreateCustomerConsumer(ICustomerRepository customerRepository, IMessageBroker messageBroker)
    {
        _customerRepository = customerRepository;
        _messageBroker = messageBroker;
    }
    
    public async Task Consume(ConsumeContext<CreateCustomer> context)
    {
        var (id, email) = context.Message;
        var customer = Customer.CreateFromUser(id, email);
        
        await _customerRepository.AddAsync(customer);
        await _messageBroker.PublishAsync(new CustomerCreated(id, email));
    }
}