using Xunit;

namespace NetStore.Tests.Shared.Integration;

public abstract class EndpointsTests : IClassFixture<OptionsProvider>, IClassFixture<TestApplicationFactory>
{
    protected HttpClient Client { get; } 
    
    protected EndpointsTests(OptionsProvider optionsProvider, TestApplicationFactory applicationFactory)
    {
        Client = applicationFactory.WithWebHostBuilder(builder => builder.ConfigureServices(services =>
        {
        })).CreateClient();
    }
}