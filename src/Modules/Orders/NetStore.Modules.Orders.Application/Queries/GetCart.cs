using NetStore.Modules.Orders.Application.DTO;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Orders.Application.Queries;

public sealed record GetCart() : IQuery<CartDto>;