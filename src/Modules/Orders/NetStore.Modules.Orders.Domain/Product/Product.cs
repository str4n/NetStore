namespace NetStore.Modules.Orders.Domain.Product;

public sealed class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string SKU { get; private set; }
    public string Size { get; private set; }
    public string Color { get; private set; }
    public double Price { get; private set; }
    public int Stock { get; set; }

    public Product(Guid id, string name, string sku, string size, string color, double price)
    {
        Id = id;
        Name = name;
        SKU = sku;
        Price = price;
        Size = size;
        Color = color;
        Stock = 0;
    }

    private Product()
    {
    }
}