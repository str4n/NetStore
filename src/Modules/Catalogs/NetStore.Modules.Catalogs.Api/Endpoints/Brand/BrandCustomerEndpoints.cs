using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Application.Queries;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Catalogs.Api.Endpoints.Brand;

internal static class BrandCustomerEndpoints
{
    private const string Route = CatalogsModule.BasePath + "/brands";
    
    public static IEndpointRouteBuilder MapBrandCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, GetAll);
        
        return app;
    }

    private static async Task<IResult> GetAll([FromServices] IQueryDispatcher queryDispatcher)
    {
        var result = await queryDispatcher.SendAsync<GetAllBrands, IEnumerable<BrandDto>>(new GetAllBrands());
        
        return Results.Ok(result);
    }
}