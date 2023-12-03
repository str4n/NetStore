using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NetStore.Modules.Catalogs.Application.DTO;
using NetStore.Modules.Catalogs.Application.Queries;
using NetStore.Shared.Abstractions.Queries;
using NetStore.Shared.Infrastructure.Auth.Policies;

namespace NetStore.Modules.Catalogs.Api.Endpoints.ProductMockup;

internal static class ProductMockupEmployeeEndpoints
{
    private const string Route = CatalogsModule.BasePath + "/mockups";
    
    public static IEndpointRouteBuilder MapProductMockupEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, GetAll).RequireAuthorization(Policies.AtLeastEmployee);
        
        return app;
    }

    private static async Task<IResult> GetAll([FromServices] IQueryDispatcher queryDispatcher)
    {
        var result = await queryDispatcher.SendAsync<GetAllProductMockups, IEnumerable<ProductMockupDto>>(new GetAllProductMockups());

        return Results.Ok(result);
    }
}