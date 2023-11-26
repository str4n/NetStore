using System.Collections;
using NetStore.Modules.Products.Core.Domain.ValueObjects;

namespace NetStore.Modules.Products.Core.Domain.Entities;

internal sealed class Product
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    private readonly List<Category> _categories = new();
    public IEnumerable<Category> Categories => _categories;
    public Discount Discount { get; private set; }
    public Price TruePrice { get; private set; }
    public Price Price => GetPriceAfterDiscount();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Product(Guid? id, Name name, Description description, IEnumerable<Category> categories, Price truePrice, DateTime createdAt, Discount discount = default)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Description = description;
        _categories = categories.ToList();
        TruePrice = truePrice;
        CreatedAt = createdAt;
        Discount = discount;
    }

    private Product()
    {
    }

    private Price GetPriceAfterDiscount()
        => TruePrice - (TruePrice * Discount / 100);

    public static Product Create(Guid id, Name name, Description description, IEnumerable<Category> categories, Price price, DateTime createdAt ,Discount discount = default)
        => new(id, name, description, categories, price, createdAt, discount);

    public void UpdateName(Name name, DateTime updatedAt)
    {
        Name = name;
        UpdatedAt = updatedAt;
    }

    public void UpdateDescription(Description description, DateTime updatedAt)
    {
        Description = description;
        UpdatedAt = updatedAt;
    }

    public void UpdatePrice(Price price, DateTime updatedAt)
    {
        TruePrice = price;
        UpdatedAt = updatedAt;
    }

    public void UpdateDiscount(Discount discount, DateTime updatedAt)
    {
        Discount = discount;
        UpdatedAt = updatedAt;
    }

    public void AddCategory(Category category, DateTime updatedAt)
    {
        _categories.Add(category);
        UpdatedAt = updatedAt;
    }

    public void RemoveCategory(Category category, DateTime updatedAt)
    {
        _categories.Remove(category);
        UpdatedAt = updatedAt;
    }
}