using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Products.Core.DTO;
using NetStore.Modules.Products.Core.Services;
using NetStore.Shared.Abstractions.SharedTypes.ValueObjects;

namespace NetStore.Modules.Products.Api.Endpoints;

internal static class ProductEndpoints
{
    private const string Route = ProductsModule.BasePath + "/products";

    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route + "/{id:guid}", Get);
        app.MapGet(Route, GetAll);

        app.MapPost(Route, Post).RequireAuthorization();
        app.MapPatch(Route + "/{id:guid}", Patch).RequireAuthorization();

        app.MapDelete(Route + "/{id:guid}", Delete).RequireAuthorization();
        
        return app;
    }
    
    private static async Task<IResult> Get([FromRoute] Guid id, IProductsService productsService)
        => Results.Ok(await productsService.GetAsync(id));
    
    private static async Task<IResult> GetAll(IProductsService productsService)
        => Results.Ok(await productsService.GetAllAsync());
    
    [Authorize(Roles = Role.Admin)]
    private static async Task<IResult> Post([FromBody] ProductDto dto, IProductsService productsService)
    {
        dto = dto with { Id = Guid.NewGuid() };
        await productsService.AddAsync(dto);
        
        return Results.Created($"https://localhost:7240/{Route}/{dto.Id}", default);
    }
    
    [Authorize(Roles = Role.Admin)]
    private static async Task<IResult> Patch([FromRoute] Guid id, [FromBody] DiscountDto discount, IProductsService productsService)
    {
        await productsService.EditDiscountAsync(id, discount.Discount);

        return Results.NoContent();
    }
    
    [Authorize(Roles = Role.Admin)]
    private static async Task<IResult> Delete([FromRoute] Guid id, IProductsService productsService)
    {
        await productsService.DeleteAsync(id);

        return Results.NoContent();
    }
}