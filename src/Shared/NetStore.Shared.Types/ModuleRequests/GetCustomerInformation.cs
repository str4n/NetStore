using NetStore.Shared.Abstractions.Modules.Requests;
using NetStore.Shared.Types.DTO;

namespace NetStore.Shared.Types.ModuleRequests;

public sealed record GetCustomerInformation(Guid CustomerId) : IModuleRequest<CustomerInformationDto>;