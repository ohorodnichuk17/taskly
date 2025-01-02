namespace Taskly_Application.Interfaces.IService;

public interface IEmailService
{
    public Task SendEmail(string email, string subject, string message);
}
