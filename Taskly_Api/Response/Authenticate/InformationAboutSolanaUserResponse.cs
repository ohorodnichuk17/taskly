namespace Taskly_Api.Response.Authenticate;

public class InformationAboutSolanaUserResponse
{
    public Guid Id { get; set; }
    public required string PublicKey { get; set; }
    public required string AvatarName { get; set; }
    public required string UserName { get; set; }
}