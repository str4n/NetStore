using NetStore.Modules.Users.Core.DTO;
using NetStore.Modules.Users.Core.Mappings;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Abstractions.Queries;

namespace NetStore.Modules.Users.Core.Queries.Handlers;

internal sealed class GetUserHandler : IQueryHandler<GetUser, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> HandleAsync(GetUser query)
        => (await _userRepository.GetAsync(query.Id)).AsDto();
}