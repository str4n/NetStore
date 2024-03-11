using MassTransit;
using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Customers.Core.Domain.Address;
using NetStore.Modules.Customers.Core.Domain.Order;
using NetStore.Modules.Customers.Core.EF;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Modules.Customers.Shared.Commands;
using NetStore.Modules.Orders.Shared.DTO;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Modules.Orders.Shared.ModuleRequests;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Customers.Core.Messaging;

internal sealed class AddOrderToHistoryConsumer : IConsumer<AddOrderToHistory>
{
    private readonly IMessageBroker _messageBroker;
    private readonly CustomersDbContext _customersDbContext;

    public AddOrderToHistoryConsumer(IMessageBroker messageBroker, CustomersDbContext customersDbContext)
    {
        _messageBroker = messageBroker;
        _customersDbContext = customersDbContext;
    }
    
    public async Task Consume(ConsumeContext<AddOrderToHistory> context)
    {
        var orderId = context.Message.OrderId;

        var orderDto = await _messageBroker.SendAsync<GetOrder, OrderDto>(new GetOrder(orderId));

        var customerId = orderDto.CustomerId;

        var order = new Order(orderDto.Id, customerId,
            orderDto.Lines.Select(x =>
                new OrderLine(x.Id, x.ProductId, x.OrderLineNumber, x.Name, x.UnitPrice, x.Quantity)), orderDto.Status, orderDto.PlaceDate, new Address(orderDto.City, orderDto.Street, orderDto.PostalCode), orderDto.ReceiverName);

        await _customersDbContext.Orders.AddAsync(order);
        await _customersDbContext.SaveChangesAsync();
    }
}