using NetStore.Modules.Customers.Core.Domain.Exceptions;

namespace NetStore.Modules.Customers.Core.Domain.Customer;

internal sealed record Name
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidNameException("Name cannot be empty.");
        }

        if (value.Length is < 2 or > 100)
        {
            throw new InvalidNameException("Name must be between 2 and 100 letters.");
        }

        Value = value;
    }

    public static implicit operator string(Name name) => name.Value;
    public static implicit operator Name(string name) => new(name);
}