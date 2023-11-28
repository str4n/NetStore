using System.Text.RegularExpressions;
using NetStore.Modules.Customers.Core.Domain.Exceptions;

namespace NetStore.Modules.Customers.Core.Domain.Customer;

internal sealed class Address
{
    private static readonly Regex PostalCodeRegex = new("^[0-9]{2}-[0-9]{3}$");
    
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }

    public Address(string city, string street, string postalCode)
    {
        
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

        if (!PostalCodeRegex.IsMatch(postalCode))
        {
            throw new InvalidAddressException("Postal code is in invalid format. Correct format: XX-XXX");
        }
        
        City = city;
        Street = street;
        PostalCode = postalCode;
    }

    private Address()
    {
    }
}