using NetStore.Modules.Orders.Application.Exceptions;
using NetStore.Modules.Orders.Domain.Payment;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Application.Commands.Handlers;

internal sealed class SetPaymentHandler : ICommandHandler<SetPayment>
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IIdentityContext _identityContext;

    public SetPaymentHandler(ICheckoutRepository checkoutRepository, IIdentityContext identityContext)
    {
        _checkoutRepository = checkoutRepository;
        _identityContext = identityContext;
    }
    
    public async Task HandleAsync(SetPayment command)
    {
        var checkout = await _checkoutRepository.GetByCustomerId(_identityContext.Id);
        
        if (checkout is null)
        {
            throw new CheckoutCartNotFoundException();
        }

        var paymentId = Guid.NewGuid();
        var isPaymentMethodValid = Enum.TryParse(command.PaymentMethod, out PaymentMethod paymentMethod);

        if (!isPaymentMethodValid)
        {
            throw new InvalidPaymentMethodException();
        }
        
        checkout.SetPayment(new Payment(paymentId, paymentMethod));
        await _checkoutRepository.UpdateAsync(checkout);
    }
}