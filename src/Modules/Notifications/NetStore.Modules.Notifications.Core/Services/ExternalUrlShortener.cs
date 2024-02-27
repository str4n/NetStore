using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using NetStore.Modules.Notifications.Core.DTO;
using NetStore.Modules.Notifications.Core.Requests;

namespace NetStore.Modules.Notifications.Core.Services;

internal sealed class ExternalUrlShortener : IUrlShortener
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ExternalUrlShortenerOptions _options;

    public ExternalUrlShortener(IHttpClientFactory httpClientFactory, IOptions<ExternalUrlShortenerOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }
    
    public async Task<string> ShortenUrl(string url)
    {
        var schema = _options.Schema;
        var host = _options.Host;
        var urlShortenerUrl = _options.UrlShortenerUrl;
        var httpClient = _httpClientFactory.CreateClient("ExternalUrlShortener");

        var result = await httpClient.PostAsJsonAsync(urlShortenerUrl, new ShortenUrlRequest(schema, host, url));
        
        var content = await result.Content.ReadFromJsonAsync<ShortenedUrlDto>();

        return content.ShortUrl;
    }
}