using NetStore.Modules.Users.Core.Domain.Entities;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Core.Services;
using NetStore.Shared.Abstractions.Auth;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands.Handlers;

internal sealed class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ITokenStorage _tokenStorage;
    private readonly IPasswordManager _passwordManager;
    private readonly IAuthenticator _authenticator;

    public SignInHandler(IUsersRepository usersRepository, ITokenStorage tokenStorage, IPasswordManager passwordManager, IAuthenticator authenticator)
    {
        _usersRepository = usersRepository;
        _tokenStorage = tokenStorage;
        _passwordManager = passwordManager;
        _authenticator = authenticator;
    }
    
    public async Task HandleAsync(SignIn command)
    {
        var user = await _usersRepository.GetByUsernameAsync(command.Username);

        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.Validate(command.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }
        
        if (user.UserState is UserState.Deleted)
        {
            throw new UserNotActiveException(user.Username);
        }

        var jwt = _authenticator.CreateToken(user.Id, user.Role, user.Email);
        _tokenStorage.Set(jwt);
    }
}