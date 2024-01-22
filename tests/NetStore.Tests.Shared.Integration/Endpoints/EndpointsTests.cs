using System.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace NetStore.Tests.Shared.Integration.Endpoints;

public class EndpointsTests : WebApplicationFactory<Program> ,IClassFixture<OptionsProvider>
{
    protected HttpClient Client { get; private set; }
    private WebApplicationFactory<Program> Factory { get; }
    
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

    protected void CreateNewAppInstance()
    {
        WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
            builder.Configure(app =>
            {
                app.UseRouting();
                app.UseEndpoints(c =>
                {
                    c.DataSources.Clear();
                });
            });
        });
        Client = Factory.CreateClient();
    }
}