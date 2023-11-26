using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Products.Core.CQRS.Commands;

internal sealed record CreateProduct(Guid Id, string Name, string Description, IEnumerable<string> Categories, double Price, int Discount) : ICommand;