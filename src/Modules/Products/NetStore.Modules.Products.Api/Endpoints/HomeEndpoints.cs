using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace NetStore.Modules.Products.Api.Endpoints;

internal static class HomeEndpoints
{
    private const string Route = ProductsModule.BasePath + "/home";
    
    public static IEndpointRouteBuilder MapHomeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Route, () => "Products API!");
        
        return app;
    }
}