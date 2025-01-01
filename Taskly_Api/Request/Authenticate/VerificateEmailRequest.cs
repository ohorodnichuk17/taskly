namespace Taskly_Api.Request.Authenticate;

public record VerificateEmailRequest(string Email, string Code);
