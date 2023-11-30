using NetStore.Modules.Catalogs.Domain.Exceptions;

namespace NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

public sealed record ProductWeight
{
    public double Value { get; }
    public WeightUnit Unit { get; }

    public ProductWeight(double value, WeightUnit unit)
    {
        if (value <= 0)
        {
            throw new ValueExceededException("Weigh cannot be negative or zero.");
        }

        Value = value;
        Unit = unit;
    }

    private ProductWeight()
    {
    }

    public ProductWeight ConvertTo(WeightUnit unit)
    {
        if (Unit == unit)
        {
            return this;
        }

        var result = unit switch
        {
            WeightUnit.Kilogram => new ProductWeight(Value/1000, WeightUnit.Kilogram),
            WeightUnit.Gram => new ProductWeight(Value * 1000, WeightUnit.Gram),
            _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
        };

        return result;
    }
}

public enum WeightUnit
{
    Gram,
    Kilogram
}