using NetStore.Modules.Catalogs.Application.Exceptions;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class UpdateCategoryHandler : ICommandHandler<UpdateCategory>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task HandleAsync(UpdateCategory command)
    {
        var category = await _categoryRepository.GetAsync(command.Id);

        if (category is null)
        {
            throw new CategoryNotFoundException();
        }

        if (!string.IsNullOrWhiteSpace(command.Name))
        {
            category.UpdateName(command.Name);
        }

        if (!string.IsNullOrWhiteSpace(command.Description))
        {
            category.UpdateDescription(command.Description);
        }

        await _categoryRepository.UpdateAsync(category);
    }
}