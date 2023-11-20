using NetStore.Modules.Users.Core.DTO;
using NetStore.Modules.Users.Core.Validators;
using NetStore.Shared.Abstractions.Auth;

namespace NetStore.Modules.Users.Core.Services;

internal sealed class IdentityService : IIdentityService
{
    private readonly IUserValidator _validator;

    public IdentityService(IUserValidator validator)
    {
        _validator = validator;
    }
    
    public Task<UserDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<JsonWebToken> SignInAsync(SignInDto dto)
    {
        throw new NotImplementedException();
    }

    public Task SignUpAsync()
    {
        throw new NotImplementedException();
    }
}