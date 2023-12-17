using NetStore.Modules.Customers.Core.Domain.Address;
using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.DTO;

namespace NetStore.Modules.Customers.Core.Mappings;

internal static class AddressMappings
{
    public static Address ToEntity(this AddressDto dto)
        => new Address(dto.City, dto.Street, dto.PostalCode);

    public static AddressDto AsDto(this Address address)
        => new AddressDto(address.City, address.Street, address.PostalCode);
}