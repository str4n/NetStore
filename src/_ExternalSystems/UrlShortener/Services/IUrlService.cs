using UrlShortener.DTO;
using UrlShortener.Request;

namespace UrlShortener.Services;

public interface IUrlService
{
    Task<ShortenedUrlDto> Shorten(ShortenUrlRequest request);
    Task<ShortenedUrlDto> Get(string code);
}