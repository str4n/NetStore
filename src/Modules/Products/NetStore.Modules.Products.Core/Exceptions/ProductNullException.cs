using NetStore.Shared.Abstractions.Exceptions;

namespace NetStore.Modules.Products.Core.Exceptions;

internal sealed class ProductNullException : ApiException
{
    public ProductNullException() : base("Product model cannot be null.")
    {
    }
}