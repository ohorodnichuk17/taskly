using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Command.EditUserProfile;

public class EditUserProfileCommandHandlerI(IUserService userService)
    : IRequestHandler<EditAvatarCommand, ErrorOr<UserEntity>>
{
    public async Task<ErrorOr<UserEntity>> Handle(EditAvatarCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserByIdAsync(request.Id);
            user.AvatarId = request.AvatarId;
            var updatedUser = await userService.UpdateUserAsync(user);
            return updatedUser;
        }
        catch (Exception ex)
        {
            return Error.Failure("Error while updating user profile ", ex.Message);
        }
    }
}