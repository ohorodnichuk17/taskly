using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Query.GetAllAvatars;

public record GetAllAvatarsQuery : IRequest<ErrorOr<List<AvatarEntity>>>;

