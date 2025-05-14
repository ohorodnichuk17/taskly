namespace Taskly_Api.Request.Challenge;

public record CreateChallengeRequest(
    string Name,
    string Description,
    DateTime StartTime,
    DateTime EndTime,
    double Points,
    bool IsActive,
    string RuleKey,
    int TargetAmount);