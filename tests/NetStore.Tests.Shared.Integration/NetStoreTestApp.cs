using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NetStore.Tests.Shared.Integration;

internal sealed class NetStoreTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }

    public NetStoreTestApp()
    {
        Client = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("test");
        }).CreateClient();
    }
}