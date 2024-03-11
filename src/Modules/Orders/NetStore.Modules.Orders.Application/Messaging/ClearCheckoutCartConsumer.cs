using MassTransit;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Shared.Commands;

namespace NetStore.Modules.Orders.Application.Messaging;

internal sealed class ClearCheckoutCartConsumer : IConsumer<ClearCheckoutCart>
{
    private readonly ICheckoutRepository _repository;

    public ClearCheckoutCartConsumer(ICheckoutRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Consume(ConsumeContext<ClearCheckoutCart> context)
    {
        var checkoutCart = await _repository.GetByCustomerId(context.Message.CustomerId);
        
        checkoutCart.Clear();

        await _repository.UpdateAsync(checkoutCart);
    }
}