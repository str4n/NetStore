namespace NetStore.Shared.Infrastructure.Messaging;

public sealed class RabbitMqOptions
{
    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}