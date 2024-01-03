using NetStore.Modules.Customers.Core.EF;
using NetStore.Modules.Customers.Core.Exceptions;
using NetStore.Modules.Customers.Core.Mappings;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Customers.Core.Commands.Handlers;

internal sealed class CompleteCustomerInformationHandler : ICommandHandler<CompleteCustomerInformation>
{
    private readonly ICustomerRepository _customerRepository;

    public CompleteCustomerInformationHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task HandleAsync(CompleteCustomerInformation command)
    {
        var customer = await _customerRepository.GetAsync(command.Id);

        if (customer.IsCompleted())
        {
            throw new ProfileInformationAlreadyCompletedException();
        }
        
        customer.CompleteInformation(command.FirstName, command.LastName, command.Address.ToEntity());

        await _customerRepository.UpdateAsync(customer);
    }
}