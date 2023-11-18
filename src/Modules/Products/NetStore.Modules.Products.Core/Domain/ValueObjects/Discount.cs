using NetStore.Modules.Products.Core.Domain.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.ValueObjects;

internal sealed record Discount
{
    public int Value { get; }

    public Discount(int value)
    {
        if (value is < 0 or > 100)
        {
            throw new InvalidDiscountException("Product discount must be in percent.");
        }

        Value = value;
    }
    
    public static implicit operator int(Discount discount) => discount.Value;
    public static implicit operator Discount(int discount) => new(discount);
}