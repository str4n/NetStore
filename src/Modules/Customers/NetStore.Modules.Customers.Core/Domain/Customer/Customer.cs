using NetStore.Shared.Types.ValueObjects;

namespace NetStore.Modules.Customers.Core.Domain.Customer;

internal sealed class Customer
{
    public Guid Id { get; private set; }
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public CustomerStatus CustomerStatus { get; private set; } = CustomerStatus.InformationNotCompleted;

    // TODO: Orders history
    // TODO: Payment methods

    private Customer(Guid id, Name firstName, Name lastName, Email email, Address address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
    }
    
    private Customer(Guid id, Email email)
    {
        Id = id;
        Email = email;
    }

    private Customer()
    {
    }

    public static Customer Create(Guid id, Name firstName, Name lastName, Email email, Address address)
        => new(id, firstName, lastName, email, address);

    public void CompleteInformation(Name firstName, Name lastName, Address address)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;

        CustomerStatus = CustomerStatus.InformationCompleted;
    }

    public static Customer CreateFromUser(Guid id, Email email)
        => new(id, email);

    public void UpdateFirstName(Name firstname) => FirstName = firstname;
    public void UpdateLastName(Name lastname) => LastName = lastname;
    public void UpdateAddress(Address address) => Address = address;

    public bool IsCompleted() => CustomerStatus is CustomerStatus.InformationCompleted;
}