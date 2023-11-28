using NetStore.Shared.Abstractions.SharedTypes.ValueObjects;

namespace NetStore.Modules.Customers.Core.Domain.Customer;

internal sealed class Customer
{
    public Guid Id { get; private set; }
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public bool IsCompleted { get; private set; }

    // TODO: Orders history
    // TODO: Payment methods

    public Customer(Guid id, Name firstName, Name lastName, Email email, Address address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
    }
    
    public Customer(Guid id, Email email)
    {
        Id = id;
        Email = email;
    }

    private Customer()
    {
    }

    public static Customer Create(Guid id, Name firstName, Name lastName, Email email, Address address)
        => new(id, firstName, lastName, email, address);

    public void UpdateAddress(Address address) => Address = address;

    public void CompleteInformation(Name firstName, Name lastName, Address address)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;

        IsCompleted = true;
    }

    public void UpdateFirstName(Name firstname) => FirstName = firstname;
    public void UpdateLastName(Name lastname) => LastName = lastname;
}