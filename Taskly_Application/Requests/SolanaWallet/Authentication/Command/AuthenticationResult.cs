namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command;

public class AuthenticationResult
{
    public string PublicKey { get; set; }

    public AuthenticationResult(string publicKey)
    {
        PublicKey = publicKey;
    }
}