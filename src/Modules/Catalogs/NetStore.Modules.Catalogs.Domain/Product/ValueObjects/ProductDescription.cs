using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

public sealed record ProductDescription
{
    public string Value { get; }

    public ProductDescription(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyValueException("Product description cannot be empty.");
        }

        if (value.Length is > 400 or < 10)
        {
            throw new ValueLenghtException("Product description must be between 10 and 400 letters,");
        }

        Value = value;
    }

    public static implicit operator string(ProductDescription value) => value.Value;
    public static implicit operator ProductDescription(string value) => new(value);
}