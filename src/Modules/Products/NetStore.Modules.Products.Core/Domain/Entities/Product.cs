using System.Collections;
using NetStore.Modules.Products.Core.Domain.ValueObjects;

namespace NetStore.Modules.Products.Core.Domain.Entities;

internal sealed class Product
{
    public Guid Id { get; private set; }
    public Name Name { get; set; }
    public Description Description { get; private set; }
    private readonly HashSet<Category> _categories;
    public IEnumerable<Category> Categories => _categories;
    public Discount Discount { get; private set; }
    private Price TruePrice { get; set; }
    public Price Price => GetPriceAfterDiscount();

    public Product(Guid? id, Name name, Description description, IEnumerable<Category> categories, Price truePrice, Discount discount = default)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Description = description;
        _categories = categories?.Any() == true ? categories.ToHashSet() : new HashSet<Category>();
        TruePrice = truePrice;
        Discount = discount;
    }

    private Price GetPriceAfterDiscount()
        => TruePrice - (TruePrice * Discount / 100);

    public static Product Create(Guid id, Name name, Description description, IEnumerable<Category> categories, Price price, Discount discount = default)
        => new(id, name, description, categories, price, discount);

    public void EditName(Name name) => Name = name;
    public void EditDescription(Description description) => Description = description;
    public void EditPrice(Price price) => TruePrice = price;
    public void EditDiscount(Discount discount) => Discount = discount;
    public void AddCategory(Category category) => _categories.Add(category);
    public void RemoveCategory(Category category) => _categories.Remove(category);
}