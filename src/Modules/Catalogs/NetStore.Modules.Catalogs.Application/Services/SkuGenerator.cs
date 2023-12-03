namespace NetStore.Modules.Catalogs.Application.Services;

internal sealed class SkuGenerator : ISkuGenerator
{
    public string Generate(string model, string category, string color, string size)
        => $"{GenerateSegment(model)}-{GenerateSegment(category)}-{GenerateSegment(color)}-{size}";

    private string GenerateSegment(string value)
        => string.Join(string.Empty, value.Take(2)).ToUpperInvariant();
}