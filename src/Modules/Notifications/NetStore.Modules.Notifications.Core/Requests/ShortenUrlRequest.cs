namespace NetStore.Modules.Notifications.Core.Requests;

public sealed record ShortenUrlRequest(string Schema, string Host, string Url);