using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Product.ValueObjects;
using NetStore.Shared.Types.Aggregate;

namespace NetStore.Modules.Catalogs.Domain.Product;

public sealed class Product : Aggregate
{
    public ProductName Name { get; private set; }
    public ProductDescription Description { get; private set; }
    public Category.Category Category { get; private set; }
    public long CategoryId { get; private set; }
    public Brand.Brand Brand { get; private set; }
    public long BrandId { get; private set; }
    public ProductModel Model { get; private set; }
    public ProductPrice NetPrice { get; private set; }
    public ProductPrice GrossPrice { get; private set; }
    public ProductFabric Fabric { get; private set; }
    public Gender Gender { get; private set; }
    public AgeCategory AgeCategory { get; private set; }
    public Size Size { get; private set; }
    public Color Color { get; private set; }
    public string SKU { get; private set; }
    public int Stock { get; private set; }

    public static Product Create(AggregateId id, ProductName name, ProductDescription description, 
        long categoryId, long brandId, ProductModel model, ProductFabric fabric, Gender gender, AgeCategory ageCategory, 
        Size size, Color color, string sku)
    {
        var product = new Product
        {
            Id = id,
            Name = name,
            Description = description,
            
            // // Does this logic belong to the Product itself? TODO: Legal module
            // GrossPrice = CalculateGrossPrice(price, ageCategory),
            
            CategoryId = categoryId,
            BrandId = brandId,
            Model = model,
            Fabric = fabric,
            Gender = gender,
            AgeCategory = ageCategory,
            Size = size,
            Color = color,
            SKU = sku,
            Stock = 0,
        };
        
        product.ClearEvents();

        return product;
    }

    #region UpdateMethods

    public void IncreaseStock(int quantity)
    {
        if (quantity < 1)
        {
            return;
        }

        Stock += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity < 1)
        {
            return;
        }

        Stock -= quantity;
    }
    public void ChangeName(ProductName name)
    {
        Name = name;
        IncrementVersion();
    }

    public void ChangeDescription(ProductDescription description)
    {
        Description = description;
        IncrementVersion();
    }

    public void ChangeCategory(Category.Category category)
    {
        Category = category;
        IncrementVersion();
    }

    public void ChangeBrand(Brand.Brand brand)
    {
        Brand = brand;
        IncrementVersion();
    }

    public void ChangeModel(ProductModel model)
    {
        Model = model;
        IncrementVersion();
    }

    internal void ChangePrice(ProductPrice netPrice, ProductPrice grossPrice)
    {
        if (GrossPrice is null && NetPrice is null)
        {
            NetPrice = netPrice;
            GrossPrice = grossPrice;
            return;
        }
        
        NetPrice = netPrice;
        GrossPrice = grossPrice;
        IncrementVersion();
    }

    public void ChangeFabric(ProductFabric fabric)
    {
        Fabric = fabric;
        IncrementVersion();
    }

    public void ChangeGender(Gender gender)
    {
        Gender = gender;
        IncrementVersion();
    }

    public void ChangeAgeCategory(AgeCategory ageCategory)
    {
        AgeCategory = ageCategory;
        IncrementVersion();
    }

    public void ChangeSize(Size size)
    {
        Size = size;
        IncrementVersion();
    }

    public void ChangeColor(Color color)
    {
        Color = color;
        IncrementVersion();
    }

    public void ChangeSKU(string sku)
    {
        SKU = sku;
        IncrementVersion();
    }
    
    #endregion
    
}