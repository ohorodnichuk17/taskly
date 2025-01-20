using ErrorOr;
using MediatR;
using Taskly_Application.Interfaces;
using Taskly_Domain.Entities;

namespace Taskly_Application.Requests.Board.Query.GetBoardById;

public class GetBoardByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetBoardByIdQuery, ErrorOr<BoardEntity>>
{
    public async Task<ErrorOr<BoardEntity>> Handle(GetBoardByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await unitOfWork.Board.GetBoardByIdAsync(request.Id);
            return result.ToErrorOr();
        }
        catch (Exception ex)
        {
            return Error.Conflict(ex.Message);
        }
    }
}