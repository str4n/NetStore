using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

public sealed record ProductFabric
{
    public string Value { get; }

    public ProductFabric(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyValueException("Product fabric cannot be empty.");
        }

        Value = value;
    }

    public static implicit operator string(ProductFabric value) => value.Value;
    public static implicit operator ProductFabric(string value) => new(value);
}
