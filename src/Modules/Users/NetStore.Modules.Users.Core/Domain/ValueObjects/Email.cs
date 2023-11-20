using System.Text.RegularExpressions;
using NetStore.Modules.Users.Core.Domain.Exceptions;

namespace NetStore.Modules.Users.Core.Domain.ValueObjects;

internal sealed record Email
{
    private static readonly Regex Regex =
        new(
            @"^(?=.{1,256})(?=.{1,64}@.{1,255}$)(?=[^@]*[A-Za-z0-9!#$%&'*+/=?^_`{|}~-][^@]*$)(?=[^@]{0,64}@.{1,255}$)^[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(\\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@([A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?\\.)+[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?$\n");
    
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailException("Email address cannot be empty.");
        }

        if (!Regex.IsMatch(value))
        {
            throw new InvalidEmailException("Invalid value syntax");
        }

        Value = value;
    }
    
    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new(email);
}