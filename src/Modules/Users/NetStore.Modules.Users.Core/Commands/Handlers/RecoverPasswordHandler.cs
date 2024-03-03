using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Core.Services;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands.Handlers;

internal sealed class RecoverPasswordHandler : ICommandHandler<RecoverPassword>
{
    private readonly IUserRepository _userRepository;
    private readonly IRecoveryTokenRepository _tokenRepository;
    private readonly IPasswordManager _passwordManager;

    public RecoverPasswordHandler(IUserRepository userRepository, IRecoveryTokenRepository tokenRepository, 
        IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _passwordManager = passwordManager;
    }
    
    public async Task HandleAsync(RecoverPassword command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        var token = await _tokenRepository.GetAsync(command.RecoveryToken);
        var password = new Password(command.NewPassword);
        
        if (user is null)
        {
            throw new UserNotFoundException();
        }
        
        if (user.State != UserState.Active)
        {
            throw new UserNotActiveException(user.Username);
        }

        if (user.Id != token.UserId)
        {
            throw new UserMismatchException();
        }

        if (_passwordManager.Validate(password.Value, user.Password.Value))
        {
            throw new InvalidPasswordException("New password cannot be the same as your old one.");
        }

        var securedPassword = _passwordManager.Secure(password.Value);

        user.Password = securedPassword;
        
        await _userRepository.UpdateAsync(user);
        await _tokenRepository.DeleteAsync(token);
    }
}