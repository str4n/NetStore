using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

internal sealed record ProductName
{
    public string Value { get; }

    public ProductName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyValueException("Product name cannot be empty.");
        }

        if (value.Length is > 200 or < 2)
        {
            throw new ValueLenghtException("Product name must be between 3 and 200 letters,");
        }

        Value = value;
    }

    public static implicit operator string(ProductName value) => value.Value;
    public static implicit operator ProductName(string value) => new(value);
}