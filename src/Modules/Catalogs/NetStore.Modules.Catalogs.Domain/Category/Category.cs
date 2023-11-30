namespace NetStore.Modules.Catalogs.Domain.Category;

internal sealed class Category
{
    public Guid Id { get; private set; }
    public CategoryName Name { get; private set; }
    public CategoryDescription Description { get; private set; }
    public string Code => GetCode();

    public static Category Create(CategoryName name, CategoryDescription description)
        => new Category()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };
    
    public void UpdateName(CategoryName name) => Name = name;
    public void UpdateDescription(CategoryDescription description) => Description = description;
    
    private string GetCode()
    {
        var codeFirstPart = Name.Value[0];
        var codeSecondPart = new Random().Next(100, 999);

        return $"{codeFirstPart}-{codeSecondPart}";
    }

    public override string ToString() => Name;
}