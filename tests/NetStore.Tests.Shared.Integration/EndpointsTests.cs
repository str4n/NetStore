using Xunit;

namespace NetStore.Tests.Shared.Integration;

public abstract class EndpointsTests : IClassFixture<OptionsProvider>
{
    protected HttpClient Client { get; } 
    
    protected EndpointsTests(OptionsProvider optionsProvider)
    {
        var app = new NetStoreTestApp();
        Client = app.Client;
    }
}