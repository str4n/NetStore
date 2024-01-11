using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Modules.Customers.Shared.DTO;
using NetStore.Modules.Customers.Shared.ModuleRequests;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Modules.Customers.Core.ModuleRequests;

internal sealed class GetCustomerInformationHandler : IModuleRequestHandler<GetCustomerInformation, CustomerInformationDto>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerInformationHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<CustomerInformationDto> HandleAsync(GetCustomerInformation request)
    {
        var customer = await _customerRepository.GetAsync(request.CustomerId);

        return new CustomerInformationDto(customer.FirstName, customer.LastName, customer.Address.City,
            customer.Address.Street);
    }
}