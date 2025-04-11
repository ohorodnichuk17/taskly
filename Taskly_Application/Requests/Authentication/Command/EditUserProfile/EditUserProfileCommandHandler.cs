using ErrorOr;
using MediatR;
using Taskly_Application.DTO.UserDTO;
using Taskly_Application.Interfaces.IService;

namespace Taskly_Application.Requests.Authentication.Command.EditUserProfile;

public class EditUserProfileCommandHandlerI(IUserService userService)
    : IRequestHandler<EditUserProfileCommand, ErrorOr<EditUserDTO>>
{
    public async Task<ErrorOr<EditUserDTO>> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetUserByEmailAsync(request.Email);
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.AvatarId = request.AvatarId;
            var updatedUser = await userService.UpdateUserAsync(user);
            var dto = new EditUserDTO(updatedUser.Email, updatedUser.UserName, updatedUser.AvatarId);
            return dto;
        }
        catch (Exception ex)
        {
            return Error.Failure("Error while updating user profile ", ex.Message);
        }
    }
}