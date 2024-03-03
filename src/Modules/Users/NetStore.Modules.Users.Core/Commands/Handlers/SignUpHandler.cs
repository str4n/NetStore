using System.Text.RegularExpressions;
using NetStore.Modules.Users.Core.Domain.Exceptions;
using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Core.Services;
using NetStore.Modules.Users.Core.Validators;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.Time;
using NetStore.Shared.Types.ValueObjects;

namespace NetStore.Modules.Users.Core.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;
    private readonly ISignUpCommandValidator _validator;
    private readonly IActivationTokenRepository _tokenRepository;

    public SignUpHandler(IUserRepository userRepository, IPasswordManager passwordManager, IClock clock, 
        IMessageBroker messageBroker, ISignUpCommandValidator validator, IActivationTokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _passwordManager = passwordManager;
        _clock = clock;
        _messageBroker = messageBroker;
        _validator = validator;
        _tokenRepository = tokenRepository;
    }
    public async Task HandleAsync(SignUp command)
    {
        var id = command.Id;
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);

        await _validator.Validate(command);

        var securedPassword = _passwordManager.Secure(password);

        var user = new User(id, email.Value.ToLowerInvariant(), username, securedPassword, Role.User, UserState.NotActive, _clock.Now());

        var token = $"{Guid.NewGuid()}";
        var activationToken = new ActivationToken(token, id);
        
        await _userRepository.AddAsync(user);
        await _tokenRepository.AddAsync(activationToken);
        await _messageBroker.PublishAsync(new UserSignedUp(user.Id, user.Email));
        await _messageBroker.PublishAsync(new AccountActivationRequested(user.Email, user.Username, token));
    }
}