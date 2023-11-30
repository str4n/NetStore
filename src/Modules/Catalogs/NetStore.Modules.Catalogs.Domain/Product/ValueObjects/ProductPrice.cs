using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

public sealed record ProductPrice
{
    public double Value { get; }

    public ProductPrice(double value)
    {
        if (value is <= 0 or > 99999)
        {
            throw new ValueExceededException("Product price must be greater than 0 and less than 99999.");
        }

        Value = value;
    }
    
    public static implicit operator double(ProductPrice productPrice) => productPrice.Value;
    public static implicit operator ProductPrice(double price) => new(price);
}