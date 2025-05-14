namespace Taskly_Api.Request.Authenticate;

public record SetUserNameForSolanaUserRequest(string PublicKey, string UserName);