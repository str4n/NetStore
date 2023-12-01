using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Application.Exceptions;
using NetStore.Modules.Catalogs.Application.Mappings;
using NetStore.Modules.Catalogs.Domain.Category;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Application.Queries.Handlers;

internal sealed class GetCategoryHandler : IQueryHandler<GetCategory, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> HandleAsync(GetCategory query)
    {
        var category = await _categoryRepository.GetByCodeAsync(query.Code);

        if (category is null)
        {
            throw new CategoryNotFoundException();
        }

        return category.AsDto();
    }
}