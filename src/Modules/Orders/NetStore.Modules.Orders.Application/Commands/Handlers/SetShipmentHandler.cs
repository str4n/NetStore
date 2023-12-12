using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Domain.Shipment;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class SetShipmentHandler : ICommandHandler<SetShipment>
{
    private readonly IIdentityContext _identityContext;
    private readonly ICheckoutRepository _checkoutRepository;

    public SetShipmentHandler(IIdentityContext identityContext, ICheckoutRepository checkoutRepository)
    {
        _identityContext = identityContext;
        _checkoutRepository = checkoutRepository;
    }
    
    public async Task HandleAsync(SetShipment command)
    {
        var checkout = await _checkoutRepository.GetByCustomerId(_identityContext.Id);
        
        if (checkout is null)
        {
            throw new CheckoutCartNotFoundException();
        }
        
        checkout.SetShipment(new Shipment(command.City, command.Street, command.PostalCode, command.ReceiverName));

        await _checkoutRepository.UpdateAsync(checkout);
    }
}