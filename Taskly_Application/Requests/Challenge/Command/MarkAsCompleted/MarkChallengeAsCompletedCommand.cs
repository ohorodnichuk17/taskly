using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Challenge.Command.MarkAsCompleted;

public record MarkChallengeAsCompletedCommand(
    Guid ChallengeId) : IRequest<ErrorOr<bool>>;