using NetStore.Modules.Customers.Core.DTO;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Customers.Core.Queries;

internal sealed record GetCustomer(Guid Id) : IQuery<CustomerDto>;