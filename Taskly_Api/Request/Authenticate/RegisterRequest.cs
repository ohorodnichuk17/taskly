namespace Taskly_Api.Request.Authenticate;

public record RegisterRequest(string Email, string Password, string ConfirmPassword, Guid AvatarId, string? ReferralCode);
