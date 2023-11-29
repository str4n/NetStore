using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Products.Core.CQRS.Commands;
using NetStore.Modules.Products.Core.CQRS.Queries;
using NetStore.Modules.Products.Core.DTO;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Queries;
using NetStore.Shared.Types.SharedTypes.ValueObjects;

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
    
    private static async Task<IResult> Get([FromRoute] Guid id, [FromServices]IQueryDispatcher queryDispatcher)
        => Results.Ok(await queryDispatcher.SendAsync<GetProduct, ProductDto>(new GetProduct(id)));
    
    private static async Task<IResult> GetAll([FromServices]IQueryDispatcher queryDispatcher)
        => Results.Ok(await queryDispatcher.SendAsync<GetAllProducts, IEnumerable<ProductDto>>(new GetAllProducts()));
    
    [Authorize(Roles = Role.Admin)]
    private static async Task<IResult> Post([FromBody] ProductDto dto, [FromServices]ICommandDispatcher commandDispatcher)
    {
        dto = dto with { Id = Guid.NewGuid() };
        await commandDispatcher.SendAsync(new CreateProduct(dto.Id, dto.Name, dto.Description, dto.Categories, dto.Price, dto.Discount));
        
        return Results.Created($"https://localhost:7240/{Route}/{dto.Id}", default);
    }
    
    [Authorize(Roles = Role.Admin)]
    private static async Task<IResult> Patch([FromRoute] Guid id, [FromBody] DiscountDto discount, [FromServices]ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(new UpdateDiscount(id, discount.Discount));

        return Results.NoContent();
    }
    
    [Authorize(Roles = Role.Admin)]
    private static async Task<IResult> Delete([FromRoute] Guid id, [FromServices]ICommandDispatcher commandDispatcher)
    {
        await commandDispatcher.SendAsync(new DeleteProduct(id));

        return Results.NoContent();
    }
}