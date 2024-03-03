using NetStore.Modules.Users.Core.Domain.User;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands.Handlers;

internal sealed class DeleteUserHandler : ICommandHandler<DeleteUser>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task HandleAsync(DeleteUser command)
    {
        var user = await _userRepository.GetAsync(command.Id);

        if (user is null)
        {
            throw new UserNotFoundException();
        }

        user.State = UserState.Deleted;

        await _userRepository.UpdateAsync(user);
    }
}