namespace NetStore.Modules.Catalogs.Domain.Category;

public sealed class Category
{
    public long Id { get; private set; }
    public CategoryName Name { get; private set; }
    public CategoryDescription Description { get; private set; }
    public string Code { get; private set; }

    public static Category Create(CategoryName name, CategoryDescription description)
    {
        var category = new Category
        {
            Name = name,
            Description = description
        };

        category.Code = category.GenerateCode();

        return category;
    }
    
    public void UpdateName(CategoryName name) => Name = name;
    public void UpdateDescription(CategoryDescription description) => Description = description;
    
    private string GenerateCode()
    {
        var codeFirstPart = Name.Value[0];
        var codeSecondPart = new Random().Next(100, 999);

        return $"{codeFirstPart}-{codeSecondPart}";
    }

    public override string ToString() => Name;
}