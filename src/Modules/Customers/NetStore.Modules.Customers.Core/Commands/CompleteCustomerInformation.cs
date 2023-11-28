using NetStore.Modules.Customers.Core.DTO;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Customers.Core.Commands;

internal sealed record CompleteCustomerInformation(Guid Id, string FirstName, string LastName, AddressDto Address) : ICommand; //TODO: Payment methods