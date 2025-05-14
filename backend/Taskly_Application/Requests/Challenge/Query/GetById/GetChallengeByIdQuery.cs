using MediatR;
using Taskly_Domain.Entities;
using ErrorOr;

namespace Taskly_Application.Requests.Challenge.Query.GetById;

public record GetChallengeByIdQuery(
    Guid Id) : IRequest<ErrorOr<ChallengeEntity>>;