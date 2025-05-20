using System.Net;
using System.Net.Mail;
using TasklySender_Application.Interfaces;
using TasklySender_Domain.Common;
using Microsoft.Extensions.Options;
using TasklySender_Domain.Constants;

namespace TasklySender_Infrastructure.Services;


public class EmailService(IOptions<EmailSettings> options) : IEmailService
{
    private readonly EmailSettings settings = options.Value;
    public async Task SendHTMLPage(string email, string typeOfHTMLPage, Dictionary<string, string> props)
    {
        var path = Path.Combine("..",
                                    "TasklySender_Infrastructure",
                                    "HTMLPages",
                                    typeOfHTMLPage,
                                    $"{typeOfHTMLPage}.html");
        var htmlBody = await File.ReadAllTextAsync(path);

        foreach (var prop in props)
        {
            var buffer = htmlBody.Split(prop.Key);
            htmlBody = string.Join(prop.Value, buffer);
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
        if (typeOfHTMLPage == Constants.VerificateEmail)
        {
            subject = "Verificate email";
        }
        else if (typeOfHTMLPage == Constants.ChangePassword)
        {
            subject = "Change password";
        }
        return subject;
    }
}

