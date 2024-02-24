using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.DTO;

namespace NetStore.Modules.Users.Core.Mappings;

internal static class UserMappings
{
    public static UserDto AsDto(this User user)
        => new UserDto(user.Id, user.Email, user.Username, user.Role);
}