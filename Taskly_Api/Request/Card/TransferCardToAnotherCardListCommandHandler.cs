using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;

namespace Taskly_Api.Request.Card;

public class TransferCardToAnotherCardListCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<TransferCardToAnotherCardListCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(TransferCardToAnotherCardListCommand request, CancellationToken cancellationToken)
    {
        var transferedCard = await unitOfWork.CardTemplates.TransferCardToAnotherCardListAsync(request.AnotherCardListId, request.CardId);

        if (transferedCard == null)
            return Error.Conflict("Something went wrong.");

        return transferedCard.Value;
    }
}
