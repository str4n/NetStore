using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Application.Mappings;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Contexts;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Orders.Application.Queries.Handlers;

internal sealed class GetCheckoutCartHandler : IQueryHandler<GetCheckoutCart, CheckoutCartDto>
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IIdentityContext _identityContext;

    public GetCheckoutCartHandler(ICheckoutRepository checkoutRepository, IIdentityContext identityContext)
    {
        _checkoutRepository = checkoutRepository;
        _identityContext = identityContext;
    }
    
    public async Task<CheckoutCartDto> HandleAsync(GetCheckoutCart query)
    {
        var checkout = await _checkoutRepository.GetByCustomerId(_identityContext.Id);

        if (checkout is null)
        {
            throw new CheckoutCartNotFoundException();
        }

        if (checkout.Shipment is not null)
        {
            return new CheckoutCartDto(default, checkout.Shipment.AsDto(), 
                checkout.Products.Select(x => x.AsDto()));
        }

        if (checkout.Payment is not null)
        {
            return new CheckoutCartDto(checkout.Payment.PaymentMethod.ToString(), default,
                checkout.Products.Select(x => x.AsDto()));
        }

        if (!checkout.IsInformationCompleted())
        {
            return new CheckoutCartDto(default, default, checkout.Products.Select(x => x.AsDto()));
        }

        return checkout.AsDto();
    }
}