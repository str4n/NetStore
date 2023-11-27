using System.Text.RegularExpressions;
using NetStore.Modules.Users.Core.Domain.Entities;
using NetStore.Modules.Users.Core.Domain.Exceptions;
using NetStore.Modules.Users.Core.Domain.ValueObjects;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Core.Services;
using NetStore.Modules.Users.Shared.Events;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Events;
using NetStore.Shared.Abstractions.Messaging;
using NetStore.Shared.Abstractions.SharedTypes.ValueObjects;
using NetStore.Shared.Abstractions.Time;

namespace NetStore.Modules.Users.Core.CQRS.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private static readonly Regex Regex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;
    private readonly IMessageBroker _messageBroker;

    public SignUpHandler(IUsersRepository usersRepository, IPasswordManager passwordManager, IClock clock, IMessageBroker messageBroker)
    {
        _usersRepository = usersRepository;
        _passwordManager = passwordManager;
        _clock = clock;
        _messageBroker = messageBroker;
    }
    public async Task HandleAsync(SignUp command)
    {
        var id = command.Id;
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);

        if (!Regex.IsMatch(password))
        {
            throw new InvalidPasswordSyntaxException("Password must contain minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.");
        }
        
        if (await _usersRepository.GetByEmailAsync(email) is not null)
        {
            throw new UserAlreadyExistsException($"User with email: {email} already exists");
        }

        if (await _usersRepository.GetByUsernameAsync(username) is not null)
        {
            throw new UserAlreadyExistsException($"User with username: {username} already exists");
        }

        var securedPassword = _passwordManager.Secure(password);

        var user = new User(id, email.Value.ToLowerInvariant(), username, securedPassword, Role.User, UserState.Active, _clock.Now());

        await _usersRepository.AddAsync(user);
        await _messageBroker.PublishAsync(new UserCreated(user.Id, user.Email));
    }
}