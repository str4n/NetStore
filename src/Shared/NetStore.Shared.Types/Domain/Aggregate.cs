using System.Collections.Concurrent;
using NetStore.Shared.Abstractions.Domain;

namespace NetStore.Shared.Types.Domain;

public abstract class Aggregate<T>
{
    public T Id { get; protected set; }
    public int Version { get; protected set; }

    public IEnumerable<IDomainEvent> Events => _events;
    private readonly List<IDomainEvent> _events = new();
    
    private bool _versionIncremented;
    
    protected void AddEvent(IDomainEvent @event)
    {
        if (_events.Any() is false && _versionIncremented is false)
        {
            Version++;
            _versionIncremented = true;
        }
        
        _events.Add(@event);
    }

    protected void ClearEvents() => _events.Clear();

    protected void IncrementVersion()
    {
        if (_versionIncremented is false)
        {
            Version++;
            _versionIncremented = true;
        }
    }
}

public abstract class Aggregate : Aggregate<AggregateId>
{
}