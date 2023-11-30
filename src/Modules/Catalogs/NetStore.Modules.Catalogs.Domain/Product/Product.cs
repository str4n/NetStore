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
    public ProductPrice NetPrice { get; private set; }
    public ProductPrice GrossPrice { get; private set; }
    public ProductFabric Fabric { get; private set; }
    public ProductWeight Weight { get; private set; }
    public Gender Gender { get; private set; }
    public AgeCategory AgeCategory { get; private set; }
    public Size Size { get; private set; }
    public Color Color { get; private set; }
    public string SKU { get; private set; }
    
    // TODO: Product state

    public static Product Create(ProductName name, ProductDescription description, ProductPrice price,
        Category.Category category, Brand.Brand brand, ProductModel model, ProductFabric fabric, ProductWeight weight,
        Gender gender, AgeCategory ageCategory, Size size, Color color, string sku)
    {
        var product = new Product
        {
            Name = name,
            Description = description,
            NetPrice = price,
            
            // Does this logic belong to the Product itself? TODO: Legal module
            GrossPrice = CalculateGrossPrice(price, ageCategory),
            
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
        
        product.ClearEvents();

        return product;
    }

    #region UpdateMethods
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

    public void ChangePrice(ProductPrice price)
    {
        NetPrice = price;
        IncrementVersion();
    }

    public void ChangeFabric(ProductFabric fabric)
    {
        Fabric = fabric;
        IncrementVersion();
    }

    public void ChangeWeight(ProductWeight weight)
    {
        Weight = weight;
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

    private static ProductPrice CalculateGrossPrice(ProductPrice price, AgeCategory ageCategory)
        => ageCategory switch
        {
            AgeCategory.Child or AgeCategory.Teenager => price.Value + price * 0.05,
            AgeCategory.Adult => price.Value + price * 0.23,
            _ => throw new ArgumentOutOfRangeException(nameof(ageCategory), ageCategory, null)
        };
}