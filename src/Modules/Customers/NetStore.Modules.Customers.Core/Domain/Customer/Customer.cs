using NetStore.Shared.Types.ValueObjects;

namespace NetStore.Modules.Customers.Core.Domain.Customer;

internal sealed class Customer
{
    public Guid Id { get; private set; }
    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Email Email { get; private set; }
    public Address.Address Address { get; private set; }
    public CustomerStatus CustomerStatus { get; private set; } = CustomerStatus.InformationNotCompleted;
    public IEnumerable<Order.Order> Orders => _orders;
    private readonly List<Order.Order> _orders = new();

    private Customer(Guid id, Name firstName, Name lastName, Email email, Address.Address address)
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

    public static Customer Create(Guid id, Name firstName, Name lastName, Email email, Address.Address address)
        => new(id, firstName, lastName, email, address);

    public void CompleteInformation(Name firstName, Name lastName, Address.Address address)
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
    public void UpdateAddress(Address.Address address) => Address = address;
    public void AddOrder(Order.Order order) => _orders.Add(order);
    public bool IsCompleted() => CustomerStatus is CustomerStatus.InformationCompleted;
}