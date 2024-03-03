namespace NetStore.Modules.Users.Core.Domain.User;

public sealed record RecoveryToken(string Token, Guid UserId);