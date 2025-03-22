using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Authentication.Query.GetInformationAboutUser;

public record GetInformationAboutUserQuery(string Email) : IRequest<ErrorOr<UserEntity>>;
