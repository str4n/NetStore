﻿namespace NetStore.Modules.Orders.Shared.DTO;

public sealed record OrderLineDto(Guid Id, Guid ProductId, int OrderLineNumber, string Name, int Quantity, double UnitPrice);