using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;

namespace Taskly_Application.Requests.Card.Command.RemoveCard;

public class RemoveCardCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RemoveCardCommand, Deleted>
{
    public async Task<Deleted> Handle(RemoveCardCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.Cards.RemoveCardFromBoardAsync(request.CardId);
        return Result.Deleted;
    }

}
