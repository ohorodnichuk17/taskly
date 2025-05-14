namespace Taskly_Api.Request.Authenticate;

public record LoginRequest(string Email, string Password, bool RememberMe);