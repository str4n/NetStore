using System.Text.RegularExpressions;
using NetStore.Modules.Orders.Domain.Exceptions;
using NetStore.Shared.Types.Exceptions;

namespace NetStore.Modules.Orders.Domain.Payment;

public sealed record CardNumber
{
    public string Value { get; }

    public CardNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidCardNumberException("Card number cannot be empty.");
        }

        if (Regex.IsMatch(value, "^\\d{16}$"))
        {
            throw new InvalidCardNumberException("Card number must contain 16 digits.");
        }

        Value = value;
    }
    
    public static implicit operator string(CardNumber cardNumber) => cardNumber.Value;
    public static implicit operator CardNumber(string cardNumber) => new(cardNumber);
}