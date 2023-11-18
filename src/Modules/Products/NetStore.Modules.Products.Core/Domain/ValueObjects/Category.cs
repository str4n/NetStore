using NetStore.Modules.Products.Core.Domain.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.ValueObjects;

internal sealed record Category
{
    private static readonly string[] AvailableCategories = { "pc", "console", "computer-part", "laptop", "camera" };
    public string Value { get; }

    public Category(string value)
    {
        if (!AvailableCategories.Contains(value.ToLowerInvariant()))
        {
            throw new InvalidProductCategoryException($"Product category must equal: [ {string.Join( ", ", AvailableCategories)} ]");
        }
        
        Value = value;
    }
    
    public static implicit operator string(Category category) => category.Value;
    public static implicit operator Category(string category) => new(category);
}