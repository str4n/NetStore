using NetStore.Modules.Users.Core.DTO;

namespace NetStore.Modules.Users.Core.Validators;

internal interface IUserValidator
{
    Task Validate(SignUpDto dto);
}