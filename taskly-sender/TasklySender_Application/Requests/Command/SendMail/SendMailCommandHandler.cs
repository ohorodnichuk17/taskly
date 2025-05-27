using ErrorOr;
using MediatR;
using TasklySender_Application.Interfaces;

namespace TasklySender_Application.Requests.Command.SendMail;

public class SendMailCommandHandler(IEmailService emailService) : IRequestHandler<SendMailCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await emailService.SendHTMLPage(request.To, request.TypeOfHTML, request.Props);
            return "Mail has been send";
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        } 
    }
}
