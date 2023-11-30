using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands;

public sealed record CreateCategory(string Name, string Description) : ICommand;