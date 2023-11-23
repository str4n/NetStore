namespace NetStore.Modules.Customers.Core.Domain.ValueObjects;

internal sealed record Address(string Country, string City, string Street, string PostalCode);