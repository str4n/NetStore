using FluentAssertions;
using NetStore.Modules.Orders.Application.Commands;
using NetStore.Modules.Orders.Application.Commands.Handlers;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Modules.Orders.Tests.Unit.Helpers.Contexts;
using NetStore.Modules.Orders.Tests.Unit.Helpers.Repositories;
using NetStore.Shared.Abstractions.Contexts;
using Xunit;

namespace NetStore.Modules.Orders.Tests.Unit.CommandHandlers;

public class CommandHandlersTests
{
    [Fact]
    public async Task Handling_Checkout_Command_Should_Succeed()
    {
        var checkoutHandler = new CheckoutHandler(_cartRepository, _checkoutRepository, _identityContext);

        await checkoutHandler.HandleAsync(new Checkout());

        var cart = await _cartRepository.GetByCustomerIdAsync(_customerId);
        var checkout = await _checkoutRepository.GetByCustomerId(_customerId);

        checkout.Products.Should().ContainEquivalentOf(cart.Products.First());
    }

    private readonly ICartRepository _cartRepository;
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IIdentityContext _identityContext;
    private readonly Guid _customerId = Guid.Parse("00000000-0000-0000-0000-000000000001");

    public CommandHandlersTests()
    {
        _cartRepository = new InMemoryCartRepository();
        _checkoutRepository = new InMemoryCheckoutRepository();
        _identityContext = new FakeIdentityContext();
    }
}