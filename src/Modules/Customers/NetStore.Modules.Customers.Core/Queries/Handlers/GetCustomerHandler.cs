using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.DTO;
using NetStore.Modules.Customers.Core.Mappings;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Customers.Core.Queries.Handlers;

internal sealed class GetCustomerHandler : IQueryHandler<GetCustomer, CustomerDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto> HandleAsync(GetCustomer query)
    {
        var customer = await _customerRepository.GetAsync(query.Id);

        if (!customer.IsCompleted())
        {
            return new CustomerDto(customer.Id, default, default, customer.Email, default, default);
        }

        return customer.AsDto();
    }
    
}