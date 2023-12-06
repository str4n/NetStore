using NetStore.Modules.Customers.Core.EF;
using NetStore.Modules.Customers.Core.Exceptions;
using NetStore.Modules.Customers.Core.Mappings;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Customers.Core.Commands.Handlers;

internal sealed class CompleteCustomerInformationHandler : ICommandHandler<CompleteCustomerInformation>
{
    private readonly ICustomersRepository _customersRepository;

    public CompleteCustomerInformationHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }
    
    public async Task HandleAsync(CompleteCustomerInformation command)
    {
        var customer = await _customersRepository.GetAsync(command.Id);

        if (customer.IsCompleted())
        {
            throw new ProfileInformationAlreadyCompletedException();
        }
        
        customer.CompleteInformation(command.FirstName, command.LastName, command.Address.ToEntity());

        await _customersRepository.UpdateAsync(customer);
    }
}