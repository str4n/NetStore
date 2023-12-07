using NetStore.Modules.Users.Core.Domain.Entities;
using NetStore.Modules.Users.Core.Exceptions;
using NetStore.Modules.Users.Core.Repositories;
using NetStore.Shared.Abstractions.Commands;

namespace NetStore.Modules.Users.Core.Commands.Handlers;

internal sealed class DeleteUserHandler : ICommandHandler<DeleteUser>
{
    private readonly IUsersRepository _usersRepository;

    public DeleteUserHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public async Task HandleAsync(DeleteUser command)
    {
        var user = await _usersRepository.GetAsync(command.Id);

        if (user is null)
        {
            throw new UserNotFoundException(command.Id);
        }

        user.State = UserState.Deleted;
        
        //TODO: jwt blacklist

        await _usersRepository.UpdateAsync(user);
    }
}