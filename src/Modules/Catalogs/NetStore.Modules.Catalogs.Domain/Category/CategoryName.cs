using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Category;

public sealed record CategoryName
{
    public string Value { get; }

    public CategoryName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyValueException("Category name cannot be empty.");
        }

        if (value.Length is > 200 or < 2)
        {
            throw new ValueLenghtException("Category name must be between 3 and 200 letters,");
        }

        Value = value;
    }

    public static implicit operator string(CategoryName value) => value.Value;
    public static implicit operator CategoryName(string value) => new(value);
}