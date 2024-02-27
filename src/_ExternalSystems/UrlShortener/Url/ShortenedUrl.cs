namespace UrlShortener.Url;

public class ShortenedUrl
{
    public Guid Id { get; } = Guid.NewGuid();
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }
    public string Code { get; set; }
}