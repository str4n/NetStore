using NetStore.Modules.Catalogs.Application.Exceptions;
using NetStore.Modules.Catalogs.Application.Services;
using NetStore.Modules.Catalogs.Domain.Product;
using NetStore.Modules.Catalogs.Domain.Product.Enums;
using NetStore.Modules.Catalogs.Domain.Product.ValueObjects;
using NetStore.Modules.Catalogs.Domain.Repositories;
using NetStore.Modules.Catalogs.Domain.Services;
using NetStore.Modules.Catalogs.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Events;
using NetStore.Shared.Abstractions.Messaging;

namespace NetStore.Modules.Catalogs.Application.Commands.Handlers;

internal sealed class CreateProductHandler : ICommandHandler<CreateProduct>
{
    private readonly IProductRepository _productRepository;
    private readonly IProductDomainService _domainService;
    private readonly ISkuGenerator _skuGenerator;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMessageBroker _messageBroker;
    private readonly IBrandRepository _brandRepository;

    public CreateProductHandler(IProductRepository productRepository, 
        IProductDomainService domainService, ISkuGenerator skuGenerator, ICategoryRepository categoryRepository, 
        IMessageBroker messageBroker, IBrandRepository brandRepository)
    {
        _productRepository = productRepository;
        _domainService = domainService;
        _skuGenerator = skuGenerator;
        _categoryRepository = categoryRepository;
        _messageBroker = messageBroker;
        _brandRepository = brandRepository;
    }
    
    public async Task HandleAsync(CreateProduct command)
    {
        var isAgeCategoryValid = Enum.TryParse(command.AgeCategory, out AgeCategory ageCategory);
        var isSizeValid = Enum.TryParse(command.Size, out Size size);
        var isColorValid = Enum.TryParse(command.Color, out Color color);
        var isGenderValid = Enum.TryParse(command.Gender, out Gender gender);
        

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

        if (!isGenderValid)
        {
            throw new InvalidProductGenderCategoryException();
        }

        var category = await _categoryRepository.GetAsync(command.CategoryId);

        if (category is null)
        {
            throw new CategoryNotFoundException();
        }

        var brand = await _brandRepository.GetAsync(command.BrandId);
        
        if (brand is null)
        {
            throw new BrandNotFoundException();
        }

        var sku = _skuGenerator.Generate(command.Model, category.Name, color.ToString(), size.ToString());

        var product = Product.Create(command.Id, command.Name, command.Description, command.CategoryId,
            command.BrandId, command.Model, command.Fabric, gender, ageCategory, size, color, sku);
        
        _domainService.SetProductPrice(product, command.NetPrice);

        await _productRepository.AddAsync(product);

        await _messageBroker.PublishAsync(new ProductCreated(product.Id, product.Name, product.SKU, product.Size.ToString(),
            product.Color.ToString(), product.GrossPrice));
    }
}