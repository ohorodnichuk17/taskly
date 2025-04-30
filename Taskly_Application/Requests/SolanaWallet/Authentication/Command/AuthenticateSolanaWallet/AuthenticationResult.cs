namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.AuthenticateSolanaWallet;

public class AuthenticationResult
{
    public string PublicKey { get; set; }
    public Guid UserId { get; set; }

    public AuthenticationResult(string publicKey, Guid userId)
    {
        PublicKey = publicKey;
        UserId = userId;
    }
}