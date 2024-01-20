using NetStore.Shared.Abstractions.Contexts;

namespace NetStore.Modules.Orders.Tests.Unit.Helpers.Contexts;

internal sealed class FakeIdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; } = true;
    public Guid Id { get; } = Guid.Parse("00000000-0000-0000-0000-000000000001");
    public string Role { get; } = "admin";
}