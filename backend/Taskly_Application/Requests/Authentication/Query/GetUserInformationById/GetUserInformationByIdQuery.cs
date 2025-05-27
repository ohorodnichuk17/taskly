using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Query.GetUserInformationById;

public record GetUserInformationByIdQuery(Guid UserId) : IRequest<ErrorOr<UserEntity>>;
