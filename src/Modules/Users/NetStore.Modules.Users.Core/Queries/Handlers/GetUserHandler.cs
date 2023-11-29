using NetStore.Modules.Users.Core.DTO;
using NetStore.Modules.Users.Core.Mappings;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Users.Core.Queries.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task<UserDto> HandleAsync(GetUser query)
        => (await _usersRepository.GetAsync(query.Id)).AsDto();
}