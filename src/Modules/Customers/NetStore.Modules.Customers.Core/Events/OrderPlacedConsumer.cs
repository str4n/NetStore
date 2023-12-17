using MassTransit;
using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Customers.Core.Domain.Address;
using NetStore.Modules.Customers.Core.Domain.Order;
using NetStore.Modules.Customers.Core.EF;
using NetStore.Modules.Customers.Core.Repositories;
using NetStore.Modules.Orders.Shared.Events;
using NetStore.Shared.Types;

namespace NetStore.Modules.Customers.Core.Events;

internal sealed class OrderPlacedConsumer : IConsumer<OrderPlaced>
{
    private readonly CustomersDbContext _customersDbContext;

    public OrderPlacedConsumer(CustomersDbContext customersDbContext)
    {
        _customersDbContext = customersDbContext;
    }
    
    public async Task Consume(ConsumeContext<OrderPlaced> context)
    {
        var message = context.Message;
        var order = new Order(message.Order.Id,message.CustomerId, 
            message.Order.Lines
                .Select(x => new OrderLine(Guid.NewGuid(), x.OrderLineNumber, x.Name, x.UnitPrice, x.Quantity)), 
            OrderStatus.Placed, message.Order.PlaceDate, 
            new Address(message.Order.City, message.Order.Street, message.Order.PostalCode), 
            message.Order.ReceiverName);

        await _customersDbContext.Orders.AddAsync(order);
        await _customersDbContext.SaveChangesAsync();
    }
}