using System.Text.RegularExpressions;
using NetStore.Modules.Orders.Domain.Exceptions;
using NetStore.Shared.Types.Exceptions;

namespace NetStore.Modules.Orders.Domain.Payment;

public sealed record CVV
{
    public string Value { get; }

    public CVV(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidCardVerificationValueException("Card number cannot be empty.");
        }

        if (Regex.IsMatch(value, "^\\d{3}$"))
        {
            throw new InvalidCardVerificationValueException("Card verification value must contain 3 digits.");
        }

        Value = value;
    }
    
    public static implicit operator string(CVV cvv) => cvv.Value;
    public static implicit operator CVV(string cvv) => new(cvv);
}