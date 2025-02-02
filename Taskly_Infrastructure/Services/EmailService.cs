using Microsoft.Extensions.Options;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
using Taskly_Domain;
using Taskly_Domain.Other;

namespace Taskly_Infrastructure.Services;

public class EmailService(IOptions<EmailSettings> options) : IEmailService
{
    private readonly EmailSettings settings = options.Value;
    public Task SendEmail(string email, string subject, string message)
    {
        var client = new SmtpClient("smtp.gmail.com", 587) //(host, port)
        {
            Credentials = new NetworkCredential(userName: settings.Email, password: settings.Password),
            EnableSsl = true
        };
        

        return client.SendMailAsync(
            new MailMessage(from: settings.Email,
                            to: email,
                            subject: subject,
                            body: message));
    }

    public async Task SendHTMLPage(string email, string typeOfHTMLPage,object? prop)
    {
        var path = Path.Combine("..",
                                    "Taskly_Infrastructure",
                                    "HTMLPages",
                                    typeOfHTMLPage,
                                    $"{typeOfHTMLPage}.html");
        var htmlBody = await File.ReadAllTextAsync(path);

        if(typeOfHTMLPage == Constants.VerificateEmail)
        {
            var buffer = htmlBody.Split("[VERIFICATION_CODE]");
            htmlBody = string.Join(prop!.ToString(),buffer);
        }
        else if (typeOfHTMLPage == Constants.ChangePassword)
        {
            var buffer = htmlBody.Split("[CHANGE_PASSWORD_KEY]");
            htmlBody = string.Join(prop!.ToString(), buffer);
        }

        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(userName: settings.Email, password: settings.Password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage()
        {
            Subject = GetSubjectByTypeOfHTMLPage(typeOfHTMLPage),
            From = new MailAddress(settings.Email),
            Body = htmlBody,
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
    private string GetSubjectByTypeOfHTMLPage(string typeOfHTMLPage)
    {
        var subject = string.Empty;
        if(typeOfHTMLPage == Constants.VerificateEmail)
        {
            subject = "Verificate email";
        }
        else if(typeOfHTMLPage == Constants.ChangePassword)
        {
            subject = "Change password";
        }
        return subject;
    }
}
