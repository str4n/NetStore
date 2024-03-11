using NetStore.Modules.Orders.Shared.DTO;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Modules.Orders.Shared.ModuleRequests;

public sealed record GetOrder(Guid OrderId) : IModuleRequest<OrderDto>;