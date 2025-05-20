using MediatR;

namespace TasklyServer_Application.Requests.Command.SendMail;

public record SendMailCommand(string Subject, string To, string Body, bool IsBodyHtml, Dictionary<string, string> Props) : IRequest;
