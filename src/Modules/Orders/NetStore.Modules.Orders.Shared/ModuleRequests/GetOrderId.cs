using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Modules.Orders.Shared.ModuleRequests;

public sealed record GetOrderId(Guid PaymentId) : IModuleRequest<Guid>;