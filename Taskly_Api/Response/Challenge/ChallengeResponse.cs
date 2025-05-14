namespace Taskly_Api.Response.Challenge;

public record ChallengeResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public bool IsCompleted { get; init; }
    public bool IsBooked { get; init; }
    public bool IsActive { get; init; }
    public string RuleKey { get; set; }
    public double Points { get; set; }
    public int TargetAmount { get; set; }
    public Guid? UserId { get; set; }
    public UserForChallengeResponse? User { get; init; }
}