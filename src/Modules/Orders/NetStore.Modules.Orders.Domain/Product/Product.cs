namespace NetStore.Modules.Orders.Domain.Product;

public sealed class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string SKU { get; private set; }
    public double Price { get; private set; }
    public ProductState State { get; private set; } = ProductState.Available;

    public Product(Guid id, string name, string sku, double price)
    {
        Id = id;
        Name = name;
        SKU = sku;
        Price = price;
    }

    private Product()
    {
    }

    public void LockProduct() => State = ProductState.Locked;
    public void UnlockProduct() => State = ProductState.Available;
}