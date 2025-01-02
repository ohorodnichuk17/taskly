using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using Taskly_Application.Interfaces;
using Taskly_Application.Interfaces.IService;
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
}
