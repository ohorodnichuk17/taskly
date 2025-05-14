using ErrorOr;
using MediatR;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Challenge.Query.GetAllAvaliable;

public record GetAllAvaliableChallengesQuery()
    : IRequest<ErrorOr<IEnumerable<ChallengeEntity>>>;