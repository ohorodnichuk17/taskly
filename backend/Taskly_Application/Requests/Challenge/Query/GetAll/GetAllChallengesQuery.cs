using Taskly_Domain.Entities;
using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Challenge.Query.GetAll;

public record GetAllChallengesQuery()
    : IRequest<ErrorOr<IEnumerable<ChallengeEntity>>>;