using ErrorOr;
using MediatR;

namespace Taskly_Application.Requests.Challenge.Command.Create;

public record CreateChallengeCommand(
    string Name,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    double Points,
    bool IsActive,
    string RuleKey,
    int TargetAmount) : IRequest<ErrorOr<Guid>>;