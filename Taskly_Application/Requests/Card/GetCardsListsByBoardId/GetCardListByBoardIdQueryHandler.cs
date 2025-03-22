using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Application.Requests.Card.GetCardsListsByBoardId;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Card.GetCardListByBoardId;

public class GetCardListByBoardIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCardListByBoardIdQuery, ErrorOr<CardListEntity[]>>
{
    public async Task<ErrorOr<CardListEntity[]>> Handle(GetCardListByBoardIdQuery request, CancellationToken cancellationToken)
    {
        var cardList = await unitOfWork.Board.GetCardListEntityByBoardIdAsync(request.BoardId);

        if (cardList == null)
            return Error.NotFound("Board is not found");

        return cardList.ToArray().ToErrorOr();
    }
}
