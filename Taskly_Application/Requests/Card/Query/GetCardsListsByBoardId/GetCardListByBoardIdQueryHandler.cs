using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Card.Query.GetCardsListsByBoardId;

public class GetCardListByBoardIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCardListByBoardIdQuery, ErrorOr<CardListEntity[]>>
{
    public async Task<ErrorOr<CardListEntity[]>> Handle(GetCardListByBoardIdQuery request, CancellationToken cancellationToken)
    {
        var isUserHasBoard = await unitOfWork.Board.IsUserHasBoardByIdAsync(request.BoardId, request.UserId);

        if (isUserHasBoard == false)
            return Error.Forbidden("User has not access to board");

        var cardList = await unitOfWork.Board.GetCardListEntityByBoardIdAsync(request.BoardId);

        if (cardList == null)
            return Error.NotFound("Board is not found");


        return cardList.ToArray().ToErrorOr();
    }
}
