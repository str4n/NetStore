using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class DeleteCategoryHandler : ICommandHandler<DeleteCategory>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task HandleAsync(DeleteCategory command)
    {
        await _categoryRepository.DeleteAsync(command.Id);
    }
}