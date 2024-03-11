using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.DTO;
using NetStore.Modules.Orders.Shared.DTO;

namespace NetStore.Modules.Customers.Core.Mappings;

internal static class CustomerMappings
{
    public static CustomerDto AsDto(this Customer customer)
        => new CustomerDto(customer.Id, customer.FirstName, customer.LastName, customer.Email,
            customer.Address.AsDto(), 
            customer.Orders
                .Select(x => new OrderDto(x.Id, x.CustomerId, x.ReceiverName, x.Address.City, x.Address.Street, x.Address.PostalCode, x.Price, x.Status,
                    x.PlaceDate, 
                    x.Lines
                        .Select(l => new OrderLineDto(l.Id, l.ProductId,l.OrderLineNumber, l.Name, l.Quantity, l.UnitPrice)))));
    
}