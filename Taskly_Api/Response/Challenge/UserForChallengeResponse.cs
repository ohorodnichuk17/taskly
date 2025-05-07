namespace Taskly_Api.Response.Challenge;

public record UserForChallengeResponse
{
    public string? UserName { get; init; } 
    public string? Email { get; init; } 
    public Guid AvatarId { get; init; } 
}