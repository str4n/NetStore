using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Payment;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class SetUpPaymentHandler : ICommandHandler<SetUpPayment>
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IIdentityContext _identityContext;

    public SetUpPaymentHandler(ICheckoutRepository checkoutRepository, IIdentityContext identityContext)
    {
        _checkoutRepository = checkoutRepository;
        _identityContext = identityContext;
    }
    
    public async Task HandleAsync(SetUpPayment command)
    {
        var customerId = _identityContext.Id;
        var checkout = await _checkoutRepository.GetByCustomerId(customerId);
        
        if (checkout is null)
        {
            throw new CheckoutCartNotFoundException();
        }
        
        var isPaymentMethodValid = Enum.TryParse(command.PaymentMethod, out PaymentMethod paymentMethod);

        if (!isPaymentMethodValid)
        {
            throw new InvalidPaymentMethodException();
        }
        
        var paymentId = Guid.NewGuid();

        var paymentGatewaySecret = $"{Guid.NewGuid()}-{Guid.NewGuid()}";
        
        checkout.SetPayment(new Payment(paymentId, paymentMethod, default, paymentGatewaySecret, false));
        await _checkoutRepository.UpdateAsync(checkout);
    }
}