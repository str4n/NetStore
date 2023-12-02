using NetStore.Modules.Catalogs.Application.Exceptions;
using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Product.Mockup;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class CreateProductMockupHandler : ICommandHandler<CreateProductMockup>
{
    private readonly IProductMockupRepository _productMockupRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductMockupHandler(IProductMockupRepository productMockupRepository, 
        IBrandRepository brandRepository, ICategoryRepository categoryRepository)
    {
        _productMockupRepository = productMockupRepository;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
    }
    
    public async Task HandleAsync(CreateProductMockup command)
    {
        var categoryId = (await _categoryRepository.GetByCodeAsync(command.CategoryCode)).Id;
        var brandId = (await _brandRepository.GetByNameAsync(command.Brand)).Id;
        var isGenderValid = Enum.TryParse(command.Gender, out Gender gender);
        
        if (categoryId is default(long))
        {
            throw new CategoryNotFoundException();
        }
        
        if (brandId is default(long))
        {
            throw new BrandNotFoundException();
        }

        if (!isGenderValid)
        {
            throw new InvalidProductGenderCategoryException();
        }

        var mockup = ProductMockup.Create(command.Name, command.Description, command.Model, command.Fabric,
            gender, categoryId, brandId);

        await _productMockupRepository.AddAsync(mockup);
    }
}