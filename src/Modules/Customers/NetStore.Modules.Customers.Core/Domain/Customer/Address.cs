using NetStore.Modules.Customers.Core.Domain.Exceptions;

namespace NetStore.Modules.Customers.Core.Domain.Customer;

internal sealed class Address
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }

    public Address(string country, string city, string street, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            throw new InvalidAddressException("Country cannot be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new InvalidAddressException("City cannot be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(street))
        {
            throw new InvalidAddressException("Street cannot be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new InvalidAddressException("Postal code cannot be empty.");
        }
        
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    private Address()
    {
    }
}