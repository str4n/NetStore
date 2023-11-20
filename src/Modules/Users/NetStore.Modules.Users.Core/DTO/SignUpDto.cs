using System.ComponentModel.DataAnnotations;

namespace NetStore.Modules.Users.Core.DTO;

internal sealed record SignUpDto(Guid Id, string Email, string Username, string Password, string Role);