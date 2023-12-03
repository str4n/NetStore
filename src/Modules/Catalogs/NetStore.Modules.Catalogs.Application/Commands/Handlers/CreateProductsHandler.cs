using NetStore.Modules.Catalogs.Application.Exceptions;
using NetStore.Modules.Catalogs.Application.Services;
using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Product.ValueObjects;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Modules.Catalogs.Domain.Services;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class CreateProductsHandler : ICommandHandler<CreateProducts>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductMockupRepository _productMockupRepository;
    private readonly IProductDomainService _domainService;
    private readonly ISkuGenerator _skuGenerator;
    private readonly ICategoryRepository _categoryRepository;

    public CreateProductsHandler(IProductRepository productRepository, IProductMockupRepository productMockupRepository, 
        IProductDomainService domainService, ISkuGenerator skuGenerator, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _productMockupRepository = productMockupRepository;
        _domainService = domainService;
        _skuGenerator = skuGenerator;
        _categoryRepository = categoryRepository;
    }
    
    public async Task HandleAsync(CreateProducts command)
    {
        var mockup = await _productMockupRepository.GetAsync(command.MockupId);
        var isAgeCategoryValid = Enum.TryParse(command.AgeCategory, out AgeCategory ageCategory);
        var isSizeValid = Enum.TryParse(command.Size, out Size size);
        var isColorValid = Enum.TryParse(command.Color, out Color color);

        if (mockup is null)
        {
            throw new ProductMockupNotFoundException();
        }

        if (!isAgeCategoryValid)
        {
            throw new InvalidProductAgeCategoryException();
        }

        if (!isSizeValid)
        {
            throw new InvalidProductSizeException();
        }

        if (!isColorValid)
        {
            throw new InvalidProductColorException();
        }
        
        var category = await _categoryRepository.GetAsync(mockup.CategoryId);

        if (category is null)
        {
            throw new CategoryNotFoundException();
        }

        List<Product> products = new();

        var sku = _skuGenerator.Generate(mockup.Model, category.Name, command.Color, command.Size);
        
        for (var i = 0; i < command.Count; i++)
        {
            var product = Product.Create(Guid.NewGuid(), mockup.Name, mockup.Description, mockup.CategoryId,
                mockup.BrandId, mockup.Model, mockup.Fabric, mockup.Gender, ageCategory, size, color,
                sku);

            _domainService.SetProductPrice(product, command.Price);
            
            products.Add(product);
        }

        await _productRepository.AddAsync(products);
    }
}