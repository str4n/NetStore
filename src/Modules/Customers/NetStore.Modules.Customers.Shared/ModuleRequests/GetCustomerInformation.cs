using NetStore.Modules.Customers.Shared.DTO;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Modules.Customers.Shared.ModuleRequests;

public sealed record GetCustomerInformation(Guid CustomerId) : IModuleRequest<CustomerInformationDto>;