namespace Taskly_Api.Request.Authenticate;

public record UpdateUserProfileForSolana(
    string PublicKey,
    Guid AvatarId,
    string Username);