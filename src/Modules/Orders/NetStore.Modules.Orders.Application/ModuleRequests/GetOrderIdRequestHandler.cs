using NetStore.Modules.Orders.Application.PaymentRegistry;
using NetStore.Modules.Orders.Shared.ModuleRequests;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Modules.Orders.Application.ModuleRequests;

internal sealed class GetOrderIdRequestHandler : IModuleRequestHandler<GetOrderId, Guid>
{
    private readonly IPaymentRegistry _paymentRegistry;

    public GetOrderIdRequestHandler(IPaymentRegistry paymentRegistry)
    {
        _paymentRegistry = paymentRegistry;
    }

    public async Task<Guid> HandleAsync(GetOrderId request)
        => (await _paymentRegistry.Get(request.PaymentId)).OrderId;
}