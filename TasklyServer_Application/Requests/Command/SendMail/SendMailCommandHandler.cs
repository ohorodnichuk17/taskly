using MediatR;

namespace TasklyServer_Application.Requests.Command.SendMail;

public class SendMailCommandHandler() : IRequestHandler<SendMailCommand>
{
    public async Task Handle(SendMailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
