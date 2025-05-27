using ErrorOr;
using MediatR;

namespace TasklySender_Application.Requests.Command.SendMail;

public record SendMailCommand(string TypeOfHTML, string To, Dictionary<string, string> Props) : IRequest<ErrorOr<string>>;
