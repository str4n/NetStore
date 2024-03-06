using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Customers.Shared.Commands;

public sealed record CreateCustomer(Guid UserId, string Email) : ICommand;