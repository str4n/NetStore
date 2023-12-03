using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Application.Queries;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Api.Endpoints.Product;

internal static class ProductCustomerEndpoints
{
    private const string Route = CatalogsModule.BasePath + "/products";
    
    public static IEndpointRouteBuilder MapProductCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, GetAll);

        return app;
    }

    private static async Task<IResult> GetAll([AsParameters] GetAllProducts query, [FromServices] IQueryDispatcher queryDispatcher)
    {
        var result = await queryDispatcher.SendAsync<GetAllProducts, IEnumerable<ProductDto>>(query);
        
        return Results.Ok(result);
    }
}