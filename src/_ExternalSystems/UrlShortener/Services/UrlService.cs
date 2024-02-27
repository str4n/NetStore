using Microsoft.EntityFrameworkCore;
using UrlShortener.DTO;
using UrlShortener.EF;
using UrlShortener.Request;
using UrlShortener.Url;

namespace UrlShortener.Services;

internal sealed class UrlService : IUrlService
{
    private readonly UrlDbContext _dbContext;
    private readonly Random _random = new();
    
    public UrlService(UrlDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ShortenedUrlDto> Shorten(ShortenUrlRequest request)
    {
        var code = await GenerateCode();
        var shortenedUrl = new ShortenedUrl
        {
            LongUrl = request.Url,
            Code = code,
            ShortUrl = $"{request.Schema}://{request.Host}/{code}"
        };

        await _dbContext.ShortenedUrls.AddAsync(shortenedUrl);

        return new ShortenedUrlDto(shortenedUrl.ShortUrl, shortenedUrl.LongUrl);
    }

    public async Task<ShortenedUrlDto> Get(string code)
    {
        var shortenedUrl = await _dbContext.ShortenedUrls.SingleOrDefaultAsync(x => x.Code == code);

        return new ShortenedUrlDto(shortenedUrl.ShortUrl, shortenedUrl.LongUrl);
    }
    
    private async Task<string> GenerateCode()
    {
        var codeChars = new char[ShortUrlSettings.GeneratedUrlLength];
        int maxValue = ShortUrlSettings.AvailableCharacters.Length;

        while (true)
        {
            for (var i = 0; i < ShortUrlSettings.GeneratedUrlLength; i++)
            {
                var randomIndex = _random.Next(maxValue);

                codeChars[i] = ShortUrlSettings.AvailableCharacters[randomIndex];
            }

            var code = new string(codeChars);

            if (!await _dbContext.ShortenedUrls.AnyAsync(x => x.Code == code))
            {
                return code;
            }
        }
    }
}