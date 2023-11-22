using NetStore.Modules.Users.Core.Domain.Entities;
using NetStore.Modules.Users.Core.Domain.ValueObjects;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Modules.Users.Core.Services;
using NetStore.Shared.Abstractions.Commands;
using NetStore.Shared.Abstractions.Time;

namespace NetStore.Modules.Users.Core.CQRS.Commands.Handlers;

internal sealed class SignUpHandler : ICommandHandler<SignUp>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IClock _clock;

    public SignUpHandler(IUsersRepository usersRepository, IPasswordManager passwordManager, IClock clock)
    {
        _usersRepository = usersRepository;
        _passwordManager = passwordManager;
        _clock = clock;
    }
    public async Task HandleAsync(SignUp command)
    {
        if (await _usersRepository.GetByEmailAsync(command.Email) is not null)
        {
            throw new UserAlreadyExistsException($"User with email: {command.Email} already exists");
        }

        if (await _usersRepository.GetByUsernameAsync(command.Username) is not null)
        {
            throw new UserAlreadyExistsException($"User with username: {command.Username} already exists");
        }

        var securedPassword = _passwordManager.Secure(command.Password);

        var user = new User(command.Id, command.Email.ToLowerInvariant(), command.Username, securedPassword, Role.User, UserState.Active, _clock.Now());

        await _usersRepository.AddAsync(user);
    }
}