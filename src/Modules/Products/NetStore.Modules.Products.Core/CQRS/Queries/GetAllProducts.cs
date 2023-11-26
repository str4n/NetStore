using NetStore.Modules.Products.Core.DTO;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Products.Core.CQRS.Queries;

internal sealed record GetAllProducts() : IQuery<IEnumerable<ProductDto>>;