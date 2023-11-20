using NetStore.Modules.Users.Core.DTO;
using NetStore.Shared.Abstractions.Auth;

namespace NetStore.Modules.Users.Core.Services;

internal interface IIdentityService
{
    Task<UserDto> GetAsync(Guid id);
    Task<JsonWebToken> SignInAsync(SignInDto dto);
    Task SignUpAsync();
}