using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Command.EditUserProfile;

public record EditAvatarCommand(
    Guid Id,
    Guid AvatarId
    ) : IRequest<ErrorOr<UserEntity>>;