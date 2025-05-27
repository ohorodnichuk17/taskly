namespace TasklySender_Application.Interfaces;

public interface IEmailService
{
    Task SendHTMLPage(string email, string typeOfHTMLPage, Dictionary<string, string> props);
}
