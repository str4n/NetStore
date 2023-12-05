using NetStore.Modules.Orders.Domain.Exceptions;

namespace NetStore.Modules.Orders.Domain.Shipment;

public sealed record Shipment
{
    public string City { get; }
    public string Street { get; }
    public string PostalCode { get; }
    public string ReceiverName { get; }
    
    //TODO: Shipment method

    public Shipment(string city, string street, string postalCode, string receiverName)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new InvalidShipmentException("City cannot be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(street))
        {
            throw new InvalidShipmentException("Street cannot be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new InvalidShipmentException("Postal code cannot be empty.");
        }
        
        if (string.IsNullOrWhiteSpace(receiverName))
        {
            throw new InvalidShipmentException("Receiver name cannot be empty.");
        }
        
        
        City = city;
        Street = street;
        PostalCode = postalCode;
        ReceiverName = receiverName;
    }
}