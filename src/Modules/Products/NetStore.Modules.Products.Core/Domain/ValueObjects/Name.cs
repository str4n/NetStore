using NetStore.Modules.Products.Core.Domain.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.ValueObjects;

internal sealed record Name
{
    public string Value { get; }

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidProductNameException("Product name cannot be empty.");
        }

        if (value.Length is < 1 or > 150)
        {
            throw new InvalidProductNameException("Product name must be between 2 and 150 letters.");
        }

        Value = value;
    }

    public static implicit operator string(Name name) => name.Value;
    public static implicit operator Name(string name) => new(name);
}