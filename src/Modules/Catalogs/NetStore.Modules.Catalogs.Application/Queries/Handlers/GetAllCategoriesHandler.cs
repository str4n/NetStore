using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Application.Mappings;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Application.Queries.Handlers;

public class GetAllCategoriesHandler : IQueryHandler<GetAllCategories, IEnumerable<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> HandleAsync(GetAllCategories query)
        => (await _categoryRepository.GetAllAsync()).Select(x => x.AsDto());
}