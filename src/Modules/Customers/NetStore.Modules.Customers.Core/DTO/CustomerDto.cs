namespace NetStore.Modules.Customers.Core.DTO;

internal sealed record CustomerDto(Guid Id, string FirstName, string LastName, string Email, IEnumerable<AddressDto> Addresses);