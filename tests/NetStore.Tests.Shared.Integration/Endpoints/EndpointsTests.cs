using System.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace NetStore.Tests.Shared.Integration.Endpoints;

public class EndpointsTests : WebApplicationFactory<Bootstrapper.Program> ,IClassFixture<OptionsProvider>
{
    protected HttpClient Client { get; private set; }
    private WebApplicationFactory<Bootstrapper.Program> Factory { get; }
    
    protected EndpointsTests(OptionsProvider optionsProvider)
    {
        Factory = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
        });
        Client = Factory.CreateClient();
    }
    
    protected void Authorize(Guid userId)
    {
        var jwt = AuthHelper.GenerateJwt(userId, "admin", "mock@gmail.com");
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
    }
}