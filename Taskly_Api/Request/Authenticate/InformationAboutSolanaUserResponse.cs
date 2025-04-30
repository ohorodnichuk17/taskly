namespace Taskly_Api.Request.Authenticate;

public class InformationAboutSolanaUserResponse
{
    public Guid Id { get; set; }
    public required string PublicKey { get; set; }
    public required Guid AvatarId { get; set; }
}