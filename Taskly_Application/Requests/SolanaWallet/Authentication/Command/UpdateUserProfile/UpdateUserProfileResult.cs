namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.UpdateUserProfile;

public class UpdateUserProfileResult
{
    public string PublicKey { get; set; }
    public Guid AvatarId { get; set; }
    public string Username { get; set; }
    
    public UpdateUserProfileResult(string publicKey, Guid avatarId, string username)
    {
        PublicKey = publicKey;
        AvatarId = avatarId;
        Username = username;
    }
}