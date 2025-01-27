using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetAllBoards;

public class GetAllBoardsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllBoardsQuery, ErrorOr<IEnumerable<BoardEntity>>>
{
    public async Task<ErrorOr<IEnumerable<BoardEntity>>> Handle(GetAllBoardsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var boards = await unitOfWork.Board.GetAllAsync();
            return boards.ToErrorOr();
        }
        catch (Exception ex)
        {
            return Error.Conflict("Error while getting all boards: ", ex.Message);
        }
    }
}