namespace NetStore.Modules.Notifications.Core.Facades;

public interface IUrlShortenerFacade
{
    Task<string> Shorten(string url);
}