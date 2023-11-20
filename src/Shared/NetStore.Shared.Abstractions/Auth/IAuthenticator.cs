namespace NetStore.Shared.Abstractions.Auth;

public interface IAuthenticator
{
    JsonWebToken CreateToken(Guid userId, string role, string email);
}