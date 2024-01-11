using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Modules.Customers.Shared.ModuleRequests;

public sealed record GetIsCustomerInformationCompleted(Guid CustomerId) : IModuleRequest<bool>;