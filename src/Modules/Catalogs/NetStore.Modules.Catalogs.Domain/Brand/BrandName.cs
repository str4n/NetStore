using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Brand;

public sealed record BrandName
{
    public string Value { get; }

    public BrandName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyValueException("Brand name cannot be empty.");
        }

        if (value.Length is > 200 or < 2)
        {
            throw new ValueLenghtException("Brand name must be between 3 and 200 letters,");
        }

        Value = value;
    }

    public static implicit operator string(BrandName value) => value.Value;
    public static implicit operator BrandName(string value) => new(value);
}