namespace Taskly_Application.Interfaces.IService;

public interface IEmailService
{
    Task SendEmail(string email, string subject, string message);
    Task SendHTMLPage(string email, string typeOfHTMLPage, object? prop);
}
