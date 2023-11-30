namespace NetStore.Shared.Types.Domain;

public record AggregateId<T>(T Value);

public sealed record AggregateId(Guid Value) : AggregateId<Guid>(Value)
{
    public AggregateId() : this(Guid.NewGuid())
    {
    }

    public static implicit operator Guid(AggregateId id) => id.Value;
    public static implicit operator AggregateId(Guid id) => new(id);
}