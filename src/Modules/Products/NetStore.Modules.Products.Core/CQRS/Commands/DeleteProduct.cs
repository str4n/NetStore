using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Products.Core.CQRS.Commands;

internal sealed record DeleteProduct(Guid Id) : ICommand;