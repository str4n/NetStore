using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace NetStore.Tests.Shared.Integration;

public class NetStoreTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }
    
    public NetStoreTestApp()
    {
        var factory = WithWebHostBuilder(builder =>
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
        Client = factory.CreateClient();
    }
}