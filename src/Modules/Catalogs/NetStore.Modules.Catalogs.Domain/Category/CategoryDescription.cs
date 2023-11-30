using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Category;

public sealed record CategoryDescription
{
    public string Value { get; }

    public CategoryDescription(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyValueException("Category description cannot be empty.");
        }

        if (value.Length is > 400 or < 10)
        {
            throw new ValueLenghtException("Category description must be between 10 and 400 letters,");
        }

        Value = value;
    }

    public static implicit operator string(CategoryDescription value) => value.Value;
    public static implicit operator CategoryDescription(string value) => new(value);
}