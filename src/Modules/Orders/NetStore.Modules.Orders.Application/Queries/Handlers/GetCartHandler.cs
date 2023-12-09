using NetStore.Modules.Orders.Application.DTO;
using NetStore.Modules.Orders.Application.Mappings;
using NetStore.Modules.Orders.Domain.Repositories;
using NetStore.Shared.Abstractions.Contexts;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Orders.Application.Queries.Handlers;

internal sealed class GetCartHandler : IQueryHandler<GetCart, CartDto>
{
    private readonly ICartRepository _cartRepository;
    private readonly IIdentityContext _identityContext;

    public GetCartHandler(ICartRepository cartRepository, IIdentityContext identityContext)
    {
        _cartRepository = cartRepository;
        _identityContext = identityContext;
    }

    public async Task<CartDto> HandleAsync(GetCart query)
        => (await _cartRepository.GetByCustomerIdAsync(_identityContext.Id)).AsDto();
}