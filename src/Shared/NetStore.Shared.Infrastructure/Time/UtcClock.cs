using NetStore.Shared.Abstractions.Time;

namespace NetStore.Shared.Infrastructure.Time;

internal sealed class UtcClock : IClock
{
    public DateTime Now() => DateTime.UtcNow;
}