namespace NetStore.Shared.Infrastructure.Postgres;

internal class UnitOfWorkTypeRegistry
{
    private static readonly Dictionary<string, Type> Types = new();

    public void Register<T>() where T : IUnitOfWork => Types[GetKey<T>()] = typeof(T);
    public Type Resolve(Type inType) => Types.TryGetValue(GetKey(inType), out var type) ? type : null;

    private string GetKey<T>()
    {
        var type = typeof(T);
        
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        var result = type.Namespace.Split(".")[2].ToLowerInvariant();

        return type.Namespace.StartsWith("NetStore.Modules.")
            ? result
            : string.Empty;
    }
    
    private string GetKey(Type type)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        var result = type.Namespace.Split(".")[2].ToLowerInvariant();

        return type.Namespace.StartsWith("NetStore.Modules.")
            ? result
            : string.Empty;
    }
}