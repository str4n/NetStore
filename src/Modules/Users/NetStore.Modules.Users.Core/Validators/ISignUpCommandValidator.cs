using NetStore.Modules.Users.Core.Commands;

namespace NetStore.Modules.Users.Core.Validators;

internal interface ISignUpCommandValidator
{
    public Task Validate(SignUp command);
}