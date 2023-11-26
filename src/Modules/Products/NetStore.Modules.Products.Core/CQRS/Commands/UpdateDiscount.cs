using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Products.Core.CQRS.Commands;

internal sealed record UpdateDiscount(Guid Id, int Discount) : ICommand;