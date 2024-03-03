namespace NetStore.Modules.Users.Core.Domain.User;

public sealed record ActivationToken(string Token, Guid UserId);