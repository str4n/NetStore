using System.Text.RegularExpressions;
using NetStore.Modules.Users.Core.Commands;
using NetStore.Modules.Users.Core.Domain.Exceptions;
using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Types.ValueObjects;

namespace NetStore.Modules.Users.Core.Validators;

internal sealed class SignUpCommandValidator : ISignUpCommandValidator
{
    private readonly IUserRepository _repository;
    private static readonly Regex Regex = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");

    public SignUpCommandValidator(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Validate(SignUp command)
    {
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(command.Password);

        if (!Regex.IsMatch(password))
        {
            throw new InvalidPasswordSyntaxException("Password must contain minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.");
        }
        
        if (await _repository.GetByEmailAsync(email) is not null)
        {
            throw new UserAlreadyExistsException($"User with email: {email.Value} already exists");
        }

        if (await _repository.GetByUsernameAsync(username) is not null)
        {
            throw new UserAlreadyExistsException($"User with username: {username.Value} already exists");
        }
    }
}