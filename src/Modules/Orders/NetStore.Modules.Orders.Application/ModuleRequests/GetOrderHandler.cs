using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.DTO;
using NetStore.Modules.Orders.Shared.ModuleRequests;
using NetStore.Shared.Abstractions.Modules.Requests;

namespace NetStore.Modules.Orders.Application.ModuleRequests;

internal sealed class GetOrderHandler : IModuleRequestHandler<GetOrder, OrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<OrderDto> HandleAsync(GetOrder request)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);

        var orderLines = order.Lines.Select(x =>
            new OrderLineDto(x.Id, x.ProductId, x.OrderLineNumber, x.Name, x.Quantity, x.UnitPrice)).ToList();

        return new OrderDto(order.Id, order.CustomerId ,order.Shipment.ReceiverName, order.Shipment.City, order.Shipment.Street,
            order.Shipment.PostalCode, order.Payment.Amount, order.Status ,order.PlaceDate, orderLines);
    }
}