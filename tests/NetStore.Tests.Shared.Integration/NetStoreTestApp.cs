using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NetStore.Tests.Shared.Integration;

internal sealed class ZNetStoreTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }

    public ZNetStoreTestApp()
    {
        Client = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("test");
        }).CreateClient();
    }
}