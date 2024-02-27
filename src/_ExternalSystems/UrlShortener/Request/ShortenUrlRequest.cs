namespace UrlShortener.Request;

public sealed record ShortenUrlRequest(string Schema, string Host, string Url);