namespace NetStore.Modules.Notifications.Core.Services;

internal interface IUrlShortener
{
    Task<string> ShortenUrl(string url);
}