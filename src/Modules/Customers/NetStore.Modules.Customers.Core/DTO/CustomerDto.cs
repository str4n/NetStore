﻿using NetStore.Modules.Orders.Shared.DTO;
namespace NetStore.Modules.Customers.Core.DTO;

internal sealed record CustomerDto(Guid Id, string FirstName, string LastName, string Email, AddressDto Address, IEnumerable<OrderDto> Orders);