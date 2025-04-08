using ErrorOr;
using MediatR;
using Taskly_Application.DTO.UserDTO;

namespace Taskly_Application.Requests.Authentication.Command.EditUserProfile;

public record EditUserProfileCommand(
    string Email,
    string UserName,
    Guid AvatarId
    ) : IRequest<ErrorOr<EditUserDTO>>;