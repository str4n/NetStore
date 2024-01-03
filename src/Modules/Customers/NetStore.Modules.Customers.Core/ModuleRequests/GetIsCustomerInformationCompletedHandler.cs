using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Shared.Abstractions.Modules.Requests;
using NetStore.Shared.Types.ModuleRequests;

namespace NetStore.Modules.Customers.Core.ModuleRequests;

internal sealed class GetIsCustomerInformationCompletedHandler : IModuleRequestHandler<GetIsCustomerInformationCompleted, bool>
{
    private readonly ICustomerRepository _customerRepository;

    public GetIsCustomerInformationCompletedHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    public async Task<bool> HandleAsync(GetIsCustomerInformationCompleted request)
    {
        var customer = await _customerRepository.GetAsync(request.CustomerId);

        return customer.IsCompleted();
    }
}