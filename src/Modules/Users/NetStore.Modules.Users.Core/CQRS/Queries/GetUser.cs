using NetStore.Modules.Users.Core.DTO;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Users.Core.CQRS.Queries;

internal sealed record GetUser(Guid Id) : IQuery<UserDto>;