using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Product.ValueObjects;
using NetStore.Shared.Types.Domain;

namespace NetStore.Modules.Catalogs.Domain.Product;

internal sealed class Product : Aggregate
{
    public ProductName Name { get; private set; }
    public ProductDescription Description { get; private set; }
    public Category.Category Category { get; private set; }
    public Brand.Brand Brand { get; private set; }
    public ProductModel Model { get; private set; }
    public ProductPrice Price { get; private set; }
    
    public ProductFabric Fabric { get; private set; }
    public ProductWeight Weight { get; private set; }
    public Gender Gender { get; private set; }
    public AgeCategory AgeCategory { get; private set; }
    public Size Size { get; private set; }
    public Color Color { get; private set; }
    public string SKU { get; private set; }

    public static Product Create(ProductName name, ProductDescription description, ProductPrice price,
        Category.Category category, Brand.Brand brand, ProductModel model, ProductFabric fabric, ProductWeight weight,
        Gender gender, AgeCategory ageCategory, Size size, Color color, string sku)
        => new Product
        {
            Name = name,
            Description = description,
            Price = price,
            Category = category,
            Brand = brand,
            Model = model,
            Fabric = fabric,
            Weight = weight,
            Gender = gender,
            AgeCategory = ageCategory,
            Size = size,
            Color = color,
            SKU = sku
        };
}