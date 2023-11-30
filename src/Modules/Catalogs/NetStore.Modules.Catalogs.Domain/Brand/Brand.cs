namespace NetStore.Modules.Catalogs.Domain.Brand;

public sealed class Brand
{
    public long Id { get; private set; }
    public BrandName Name { get; private set; }

    public static Brand Create(BrandName name)
        => new Brand
        {
            Name = name
        };

    public void ChangeName(BrandName name) => Name = name;
}