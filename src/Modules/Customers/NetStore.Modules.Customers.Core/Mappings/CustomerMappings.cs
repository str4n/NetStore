﻿using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.DTO;

namespace NetStore.Modules.Customers.Core.Mappings;

internal static class CustomerMappings
{
    public static CustomerDto AsDto(this Customer customer)
        => new CustomerDto(customer.Id, customer.FirstName, customer.LastName, customer.Email,
            customer.Address.AsDto());
}