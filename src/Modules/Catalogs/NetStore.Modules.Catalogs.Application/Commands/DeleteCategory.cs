using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands;

public sealed record DeleteCategory(long Id) : ICommand;
