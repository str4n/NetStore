using NetStore.Modules.Customers.Core.Domain.ValueObjects;
using NetStore.Shared.Abstractions.SharedTypes.ValueObjects;

namespace NetStore.Modules.Customers.Core.Domain.Entities;

internal sealed class Customer
{
    public Guid Id { get; private set; }
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Email Email { get; private set; }
    public IEnumerable<Address> Addresses => _addresses;
    private List<Address> _addresses = new();
    public Guid UserId { get; private set; }

    // TODO: Orders history
    // TODO: Payment methods

    public Customer(Guid id, Name firstName, Name lastName, Email email, Address address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        _addresses.Add(address);
    }
    
    public Customer(Guid id, Email email, Guid userId)
    {
        Id = id;
        Email = email;
        UserId = userId;
    }

    private Customer()
    {
    }

    public static Customer Create(Guid id, Name firstName, Name lastName, Email email, Address address)
        => new(id, firstName, lastName, email, address);

    public void AddAddress(Address address) => _addresses.Add(address);
    public void RemoveAddress(Address address) => _addresses.Add(address);

    public void UpdateFirstName(Name firstname) => FirstName = firstname;
    public void UpdateLastName(Name lastname) => LastName = lastname;
}