using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands;

public sealed record UpdateCategory(long Id, string Name, string Description) : ICommand;