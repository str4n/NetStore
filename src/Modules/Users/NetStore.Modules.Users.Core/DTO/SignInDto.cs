using System.ComponentModel.DataAnnotations;

namespace NetStore.Modules.Users.Core.DTO;

internal sealed record SignInDto(string Username, string Password);
