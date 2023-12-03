namespace NetStore.Modules.Catalogs.Application.Services;

internal interface ISkuGenerator
{
    string Generate(string model, string category, string color, string size);
}