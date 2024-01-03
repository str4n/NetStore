using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Shared.Types.ModuleRequests;

public sealed record GetIsCustomerInformationCompleted(Guid CustomerId) : IModuleRequest<bool>;