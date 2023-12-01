using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Product.ValueObjects;

namespace NetStore.Modules.Catalogs.Domain.Product.Mockup;

public sealed class ProductMockup
{
    public long Id { get; set; }
    public ProductName Name { get;  set; }
    public ProductDescription Description { get;  set; }
    public long CategoryId { get;  set; }
    public long BrandId { get;  set; }
    public ProductModel Model { get;  set; }
    public ProductFabric Fabric { get;  set; }
    public Gender Gender { get;  set; }
}