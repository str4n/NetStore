using Microsoft.AspNetCore.Mvc;
using NetStore.Modules.Products.Core.DTO;
using NetStore.Modules.Products.Core.Services;
using NetStore.Shared.Infrastructure.Api;

namespace NetStore.Modules.Products.Api.Controllers;

[Route(ProductsModule.BasePath + "/[controller]")]
internal sealed class ProductsController : BaseController
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Get([FromRoute] Guid id)
        => OkOrNotFound(await _productsService.GetAsync(id));
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        => Ok(await _productsService.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProductDto dto)
    {
        dto = dto with { Id = Guid.NewGuid() };
        await _productsService.AddAsync(dto);

        return CreatedAtAction(nameof(Get), new { id = dto.Id }, default);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult> Patch([FromRoute] Guid id, [FromBody] DiscountDto discount)
    {
        await _productsService.EditDiscountAsync(id, discount.Discount);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id)
    {
        await _productsService.DeleteAsync(id);

        return NoContent();
    }
}