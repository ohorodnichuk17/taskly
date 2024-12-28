namespace Taskly_Application.Interfaces;

public interface IEmailService
{
    public Task SendEmail(string email, string subject, string message);
}
