using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Command.EditUserProfile;

public record EditUserProfileCommand(
    Guid Id,
    string UserName,
    Guid AvatarId
    ) : IRequest<ErrorOr<UserEntity>>;