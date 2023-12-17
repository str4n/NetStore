using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.DTO;
using NetStore.Modules.Customers.Core.Mappings;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Customers.Core.Queries.Handlers;

internal sealed class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDto>
{
    private readonly ICustomersRepository _customersRepository;

    public GetCustomerHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<CustomerDto> HandleAsync(GetCustomer query)
    {
        var customer = await _customersRepository.GetAsync(query.Id);

        if (!customer.IsCompleted())
        {
            return new CustomerDto(customer.Id, default, default, customer.Email, default, default);
        }

        return customer.AsDto();
    }
    
}