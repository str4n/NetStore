using NetStore.Modules.Catalogs.Domain.Category;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class CreateCategoryHandler : ICommandHandler<CreateCategory>
{
    private readonly ICategoryRepository _repository;

    public CreateCategoryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }
    public async Task HandleAsync(CreateCategory command)
    {
        var category = Category.Create(command.Name, command.Description);

        await _repository.AddAsync(category);
    }
}