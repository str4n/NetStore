using NetStore.Modules.Products.Core.Domain.Exceptions;

namespace NetStore.Modules.Products.Core.Domain.ValueObjects;

internal sealed record Price
{
    public double Value { get; }

    public Price(double value)
    {
        if (value is < 0 or > 999999)
        {
            throw new InvalidProductPriceException("Product price is invalid.");
        }

        Value = value;
    }
    
    public static implicit operator double(Price price) => price.Value;
    public static implicit operator Price(double price) => new(price);
}