using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Card.Command.ChangeCard;

public class ChangeCardCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeCardCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(ChangeCardCommand request, CancellationToken cancellationToken)
    {
        var result = await unitOfWork.Cards.ChangeCardAsync(request.CardId, request.ChangeCardProps);

        if (result == null)
            return Error.NotFound("Card id not found");

        return Result.Updated;
    }
}
