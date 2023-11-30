using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

public sealed record ProductModel
{
    public string Value { get; }

    public ProductModel(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyValueException("Product model name cannot be empty.");
        }

        if (value.Length is > 200 or < 2)
        {
            throw new ValueLenghtException("Product model name must be between 3 and 200 letters,");
        }

        Value = value;
    }

    public static implicit operator string(ProductModel value) => value.Value;
    public static implicit operator ProductModel(string value) => new(value);
}