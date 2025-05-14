using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Challenge.Query.GetAllActive;

public record GetAllActiveChallengesQuery()
    : IRequest<ErrorOr<IEnumerable<ChallengeEntity>>>;