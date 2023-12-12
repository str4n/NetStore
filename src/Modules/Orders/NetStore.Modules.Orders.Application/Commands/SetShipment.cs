using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Orders.Application.Commands;

public sealed record SetShipment(string City, string Street, string PostalCode, string ReceiverName) : ICommand;