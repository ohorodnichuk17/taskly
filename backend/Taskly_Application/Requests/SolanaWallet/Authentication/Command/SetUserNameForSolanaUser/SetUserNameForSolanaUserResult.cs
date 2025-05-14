namespace Taskly_Application.Requests.SolanaWallet.Authentication.Command.SetUserNameForSolanaUser;

public class SetUserNameForSolanaUserResult
{
    public string UserName { get; set; }

    public SetUserNameForSolanaUserResult(string userName)
    {
        UserName = userName;
    }
}