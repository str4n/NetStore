using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Application.Queries;
using NetStore.Modules.Catalogs.Application.Queries.Handlers;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Api.Endpoints;

internal static class CategoryCustomerEndpoints
{
    private const string Route = CatalogsModule.BasePath + "/categories";
    
    public static IEndpointRouteBuilder MapCategoryCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route + "/all", GetAll);
        app.MapGet(Route, GetByCode); // Don't know why i doesnt work ???

        return app;
    }
    
    private static async Task<IResult> GetAll([FromServices] IQueryDispatcher queryDispatcher)
    {
        var result = await queryDispatcher.SendAsync<GetAllCategories, IEnumerable<CategoryDto>>(new GetAllCategories());

        return Results.Ok(result);
    }
    
    private static async Task<IResult> GetByCode([AsParameters]GetCategory query, [FromServices] IQueryDispatcher queryDispatcher)
    {
        var result = await queryDispatcher.SendAsync<GetCategory, CategoryDto>(query);

        return result is not null ? Results.Ok(result) : Results.NotFound();
    }
}