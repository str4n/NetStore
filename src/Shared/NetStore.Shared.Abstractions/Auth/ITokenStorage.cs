namespace NetStore.Shared.Abstractions.Auth;

public interface ITokenStorage
{
    void Set(JsonWebToken token);
    JsonWebToken Get();
}