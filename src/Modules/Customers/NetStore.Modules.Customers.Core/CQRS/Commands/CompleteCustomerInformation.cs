using NetStore.Modules.Customers.Core.DTO;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Customers.Core.CQRS.Commands;

internal sealed record CompleteCustomerInformation(Guid Id, string FirstName, string LastName, IEnumerable<AddressDto> Addresses) : ICommand; //TODO: Payment methods