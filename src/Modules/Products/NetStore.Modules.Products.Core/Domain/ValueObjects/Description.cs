using NetStore.Modules.Products.Core.Domain.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.ValueObjects;

internal sealed record Description
{
    public string Value { get; }

    public Description(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidProductDescriptionException("Product description cannot be empty.");
        }
        
        if (value.Length is < 1 or > 600)
        {
            throw new InvalidProductNameException("Product description must be between 2 and 600 letters.");
        }
        
        Value = value;
    }
    
    public static implicit operator string(Description description) => description.Value;
    public static implicit operator Description(string description) => new(description);
}