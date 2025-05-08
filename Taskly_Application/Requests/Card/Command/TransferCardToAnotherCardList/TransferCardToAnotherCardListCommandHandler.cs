using ErrorOr;
using MediatR;
using System;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Card.Command.TransferCardToAnotherCardList;

public class TransferCardToAnotherCardListCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<TransferCardToAnotherCardListCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(TransferCardToAnotherCardListCommand request, CancellationToken cancellationToken)
    {
        var transferedCard = await unitOfWork.Cards.TransferCardToAnotherCardListAsync(request.ToCardListId, request.CardId);

        if (transferedCard == null)
            return Error.Conflict("Something went wrong.");

        return transferedCard.Id;
    }
}
