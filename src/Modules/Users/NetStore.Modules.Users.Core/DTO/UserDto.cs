namespace NetStore.Modules.Users.Core.DTO;

internal sealed record UserDto(Guid Id, string Email, string Username, string Role);