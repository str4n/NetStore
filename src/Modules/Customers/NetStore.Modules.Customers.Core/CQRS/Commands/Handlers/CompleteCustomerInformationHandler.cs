﻿using NetStore.Modules.Customers.Core.EF;
using NetStore.Modules.Customers.Core.Exceptions;
using NetStore.Modules.Customers.Core.Mappings;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Customers.Core.CQRS.Commands.Handlers;

internal sealed class CompleteCustomerInformationHandler : ICommandHandler<CompleteCustomerInformation>
{
    private readonly ICustomersRepository _customersRepository;
    private readonly CustomersDbContext _dbContext;

    public CompleteCustomerInformationHandler(ICustomersRepository customersRepository, CustomersDbContext dbContext)
    {
        _customersRepository = customersRepository;
        _dbContext = dbContext;
    }
    
    public async Task HandleAsync(CompleteCustomerInformation command)
    {
        var customer = await _customersRepository.GetByUserId(command.Id);

        if (customer.IsCompleted)
        {
            throw new ProfileInformationAlreadyCompletedException();
        }
        
        customer.CompleteInformation(command.FirstName, command.LastName, command.Addresses.Select(x => x.ToEntity()));

        _dbContext.Customers.Update(customer);
        _dbContext.Addresses.AddRange(customer.Addresses);
    }
}